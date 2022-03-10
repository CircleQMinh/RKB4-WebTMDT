using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Linq.Expressions;
using WebTMDT_API.Authorize;
using WebTMDT_API.Data;
using WebTMDT_API.Helper;
using WebTMDT_API.Repository;
using WebTMDTLibrary.DTO;
using WebTMDTLibrary.Helper;

namespace WebTMDT_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IAuthManager authManager;
        private readonly UserManager<AppUser> userManager;
        public StatisticController(IUnitOfWork _unitOfWork, IMapper _mapper, UserManager<AppUser> _userManager, IAuthManager _authManager)
        {
            userManager = _userManager;
            unitOfWork = _unitOfWork;
            mapper = _mapper;
            authManager = _authManager;
        }
        [HttpGet("DashboardInfo")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetDashboardInfo()
        {
            try
            {
                var userCount = await unitOfWork.Users.GetCount(null);
                var productCount = await unitOfWork.Books.GetCount(null);
                var orderCount = await unitOfWork.Orders.GetCount(q => q.Status == (int)OrderStatus.Done);
                var uncheckOrderCount = await unitOfWork.Orders.GetCount(q => q.Status == (int)OrderStatus.NotChecked);


                return Accepted(new { success = true, userCount, productCount, orderCount, uncheckOrderCount });
            }
            catch (Exception ex)
            {
                //return new StatusCodeResult(StatusCodes.Status500InternalServerError);
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("SaleStatistic")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetSaleStatistic(string from, string to)
        {
            DateTime dfrom = DateTime.ParseExact(from, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime dto = DateTime.ParseExact(to, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            dto = dto.AddDays(1);

            var test = dto.ToLongDateString();
            try
            {
                var orders = await unitOfWork.Orders.GetAll(q => q.Status == (int)OrderStatus.Done && q.ShippedDate > dfrom && q.ShippedDate < dto, null, null);
                var dic = new Dictionary<string, double>();
                foreach (var order in orders)
                {
                    if (!dic.ContainsKey(order.ShippedDate.ToShortDateString()))
                    {
                        dic.Add(order.ShippedDate.ToShortDateString(), order.TotalPrice);
                    }
                    else
                    {
                        dic[order.ShippedDate.ToShortDateString()] += order.TotalPrice;
                    }
                }

                var sort = dic.OrderBy(item => DateTime.ParseExact(item.Key, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                var sortedDict = sort.ToDictionary(pair => pair.Key, pair => pair.Value);
                return Accepted(new { result = sortedDict, success = true });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("OrderStatistic")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetOrderStatistic(string from, string to)
        {
            DateTime dfrom = DateTime.ParseExact(from, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime dto = DateTime.ParseExact(to, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            dto = dto.AddDays(1);


            if (dfrom > dto)
            {
                return Ok(new { error = "Dữ liệu nhập không hợp lệ", success = true });
            }

            try
            {
                var orders = await unitOfWork.Orders.GetAll(q => q.Status == (int)OrderStatus.Done && q.ShippedDate > dfrom && q.ShippedDate <= dto, null, null);
                var dic = new Dictionary<string, int>();
                foreach (var order in orders)
                {
                    if (!dic.ContainsKey(order.ShippedDate.ToShortDateString()))
                    {
                        dic.Add(order.ShippedDate.ToShortDateString(), 1);
                    }
                    else
                    {
                        dic[order.ShippedDate.ToShortDateString()] += 1;
                    }
                }
                var sort = dic.OrderBy(item => DateTime.ParseExact(item.Key, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                var sortedDict = sort.ToDictionary(pair => pair.Key, pair => pair.Value);
                return Accepted(new { result = sortedDict, success = true });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("TopProduct")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetTopProduct(int numberOfBook)
        {
            try
            {
                var orderDetails = await unitOfWork.OrderDetails.GetAll(q => q.Order.Status == (int)OrderStatus.Done, null, new List<string> { "Book" });
                Dictionary<int, int> dic = new Dictionary<int, int>();
                foreach (var od in orderDetails)
                {
                    if (!dic.ContainsKey(od.BookId))
                    {
                        dic.Add(od.BookId, od.Quantity);
                    }
                    else
                    {
                        dic[od.BookId] += od.Quantity;
                    }
                }
                List<PopularBookDTO> result = new List<PopularBookDTO>();
                foreach (var item in dic)
                {
                    var od = orderDetails.Where(q => q.BookId == item.Key).FirstOrDefault();
                    var rs = new PopularBookDTO() { Book = mapper.Map<BookDTO>(od.Book), Sales = item.Value };
                    result.Add(rs);
                }

                result.Sort((a, b) => b.Sales - a.Sales); //Desc
                result = result.Take(numberOfBook).ToList();

                foreach (var item in result)
                {
                    var temp = await unitOfWork.Books.Get(q => q.Id == item.Book.Id, new List<string> { "Authors", "Genres", "Publisher", "PromotionInfo", "WishlistUsers" });
                    item.Book = mapper.Map<BookDTO>(temp);
                }


                return Ok(new { result }); ;
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.ToString() });
            }
        }

        [HttpGet("search")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> FindAll(string type, string searchBy, string keyword, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                Expression<Func<Book, bool>> expression_book = null;
                Expression<Func<Order, bool>> expression_order = null;
                dynamic listFromQuery;
                dynamic result;
                int count = 0;

                switch (type, searchBy)
                {
                    //--------------------------------------------------------------------------------------------------
                    case ("Product", "Name"):
                        expression_book = q => q.Title.Contains(keyword);
                        listFromQuery = await unitOfWork.Books.GetAll(
                        expression_book,
                        null,
                        new List<string> { "Authors", "Genres", "Publisher", "PromotionInfo" },
                        new PaginationFilter(pageNumber, pageSize));
                        count = await unitOfWork.Books.GetCount(expression_book);
                        result = mapper.Map<IList<BookDTO>>(listFromQuery);
                        return Accepted(new { success = true, result = result, total = count });
                    case ("Product", "Price"):
                        expression_book = q => q.Price == Int32.Parse(keyword);
                        listFromQuery = await unitOfWork.Books.GetAll(
                        expression_book,
                        null,
                        new List<string> { "Authors", "Genres", "Publisher", "PromotionInfo", "WishlistUsers" },
                        new PaginationFilter(pageNumber, pageSize));
                        count = await unitOfWork.Books.GetCount(expression_book);
                        result = mapper.Map<IList<BookDTO>>(listFromQuery);
                        return Accepted(new { success = true, result = result, total = count });
                    case ("Product", "Id"):
                        expression_book = q => q.Id == Int32.Parse(keyword);
                        listFromQuery = await unitOfWork.Books.GetAll(
                        expression_book,
                        null,
                        new List<string> { "Authors", "Genres", "Publisher", "PromotionInfo", "WishlistUsers" },
                        new PaginationFilter(pageNumber, pageSize));
                        count = await unitOfWork.Books.GetCount(expression_book);
                        result = mapper.Map<IList<BookDTO>>(listFromQuery);
                        return Accepted(new { success = true, result = result, total = count });
                    //--------------------------------------------------------------------------------------------------
                    case ("Order", "Name"):
                        expression_order = q => q.ContactName.Contains(keyword);
                        listFromQuery = await unitOfWork.Orders.GetAll(
                          expression_order,
                          null,
                          new List<string> { "OrderDetails", "Shipper", "DiscountCode" },
                          new PaginationFilter(pageNumber, pageSize));
                        count = await unitOfWork.Orders.GetCount(expression_order);
                        result = mapper.Map<IList<OrderDTO>>(listFromQuery);
                        return Accepted(new { success = true, result = result, total = count });


                    case ("Order", "Id"):
                        expression_order = q => q.Id == Int32.Parse(keyword);
                        listFromQuery = await unitOfWork.Orders.GetAll(
                          expression_order,
                          null,
                          new List<string> { "OrderDetails", "Shipper", "DiscountCode" },
                          new PaginationFilter(pageNumber, pageSize));
                        count = await unitOfWork.Orders.GetCount(expression_order);
                        result = mapper.Map<IList<OrderDTO>>(listFromQuery);
                        return Accepted(new { success = true, result = result, total = count });
                    case ("Order", "TotalPrice"):
                        expression_order = q => q.TotalPrice == Int32.Parse(keyword);
                        listFromQuery = await unitOfWork.Orders.GetAll(
                          expression_order,
                          null,
                          new List<string> { "OrderDetails", "Shipper", "DiscountCode" },
                          new PaginationFilter(pageNumber, pageSize));
                        count = await unitOfWork.Orders.GetCount(expression_order);
                        result = mapper.Map<IList<OrderDTO>>(listFromQuery);
                        return Accepted(new { success = true, result = result, total = count });
                    case ("Order", "OrderDate"):

                        expression_order = q => q.OrderDate.Date.Equals(DateTime.ParseExact(keyword, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date);
                        listFromQuery = await unitOfWork.Orders.GetAll(
                          expression_order,
                          null,
                          new List<string> { "OrderDetails", "Shipper", "DiscountCode" },
                          new PaginationFilter(pageNumber, pageSize));
                        count = await unitOfWork.Orders.GetCount(expression_order);
                        result = mapper.Map<IList<OrderDTO>>(listFromQuery);
                        return Accepted(new { success = true, result = result, total = count });
                    case ("Order", "ShippedDate"):
                        expression_order = q => q.ShippedDate.ToShortDateString() ==
                        DateTime.ParseExact(keyword, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToShortDateString();
                        listFromQuery = await unitOfWork.Orders.GetAll(
                          expression_order,
                          null,
                          new List<string> { "OrderDetails", "Shipper", "DiscountCode" },
                          new PaginationFilter(pageNumber, pageSize));
                        count = await unitOfWork.Orders.GetCount(expression_order);
                        result = mapper.Map<IList<OrderDTO>>(listFromQuery);
                        return Accepted(new { success = true, result = result, total = count });
                    //--------------------------------------------------------------------------------------------------
                
                    default:
                        return Accepted(new { success = false, error = "Dữ liệu không hợp lệ" });
                }
            }
            catch (Exception ex)
            {
                //return new StatusCodeResult(StatusCodes.Status500InternalServerError);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("search/user")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetSearchResult_User(string searchBy, string keyword, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                Func<IQueryable<AppUser>, IOrderedQueryable<AppUser>> orderBy = null;
                Expression<Func<AppUser, bool>> expression_user = null;
                switch (searchBy)
                {
                    case "Id":
                        expression_user = q => q.Id == keyword;
                        break;
                    case "Name":
                        expression_user = q => q.UserName.Contains(keyword);
                        break;
                    case "Email":
                        expression_user = q => q.Email.Contains(keyword);
                        break;
                }

                var users = await unitOfWork.Users.GetAll(expression_user, orderBy, null, new PaginationFilter(pageNumber, pageSize));
                var count = await unitOfWork.Users.GetCount(expression_user);
                var result = mapper.Map<IList<SimpleUserForAdminDTO>>(users);
                var user_result = users.Zip(result, (u, r) => new { User = u, Result = r });
                foreach (var ur in user_result)
                {
                    var roles = await userManager.GetRolesAsync(ur.User);
                    ur.Result.Roles = roles;
                }


                return Accepted(new { success = true, result = result, total = count });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
