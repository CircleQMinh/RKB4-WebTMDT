using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using WebTMDT_API.Authorize;
using WebTMDT_API.Data;
using WebTMDT_API.Helper;
using WebTMDT_API.Repository;
using WebTMDTLibrary.DTO;

namespace WebTMDT_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IAuthManager authManager;
        private readonly UserManager<AppUser> userManager;
        public UserController(IUnitOfWork _unitOfWork, IMapper _mapper, UserManager<AppUser> _userManager, IAuthManager _authManager)
        {
            userManager = _userManager;
            unitOfWork = _unitOfWork;
            mapper = _mapper;
            authManager = _authManager;
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetUser(string orderby, string sort, int pageNumber, int pageSize)
        {
            try
            {
                Func<IQueryable<AppUser>, IOrderedQueryable<AppUser>> orderBy = null;
                switch (orderby)
                {
                    case "Id":
                        orderBy = (sort == "Asc") ? q => q.OrderBy(p => p.Id) : q => q.OrderByDescending(p => p.Id);
                        break;
                    case "Name":
                        orderBy = (sort == "Asc") ? q => q.OrderBy(p => p.UserName) : q => q.OrderByDescending(p => p.UserName);
                        break;
                    case "Email":
                        orderBy = (sort == "Asc") ? q => q.OrderBy(p => p.Email) : q => q.OrderByDescending(p => p.Email);
                        break;
                    default:
                        orderBy = (sort == "Asc") ? q => q.OrderBy(p => p.Id) : q => q.OrderByDescending(p => p.Id);
                        break;
                }

                var users = await unitOfWork.Users.GetAll(null, orderBy, null, new PaginationFilter(pageNumber, pageSize));
                var result = mapper.Map<IList<SimpleUserForAdminDTO>>(users);

                var user_result = users.Zip(result, (u, r) => new { User = u, Result = r });
                foreach (var ur in user_result)
                {
                    var roles = await userManager.GetRolesAsync(ur.User);
                    ur.Result.Roles = roles;
                }

                //result = result.Where(u => u.Roles.Contains("User")).ToList();
                var count = await unitOfWork.Users.GetCount(null);
                return Ok(new { success = true, result, total = count });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{id}/history")]
        [Authorize]
        public async Task<IActionResult> GetUserOrderHistory(string id, string status, string orderby, string sort, int pageNumber, int pageSize)
        {
            try
            {
                Expression<Func<Order, bool>> expression = q => q.UserID == id;
                Expression<Func<Order, bool>> expression_status = status == "all" ? q => true : q => q.Status == int.Parse(status);
                expression = expression.AndAlso(expression_status);

                Func<IQueryable<Order>, IOrderedQueryable<Order>> orderBy = null;
                switch (orderby)
                {
                    case "Id":
                        orderBy = (sort == "Asc") ? q => q.OrderBy(order => order.Id) : q => q.OrderByDescending(order => order.Id);
                        break;
                    case "totalPrice":
                        orderBy = (sort == "Asc") ? q => q.OrderBy(order => order.TotalPrice) : q => q.OrderByDescending(order => order.TotalPrice);
                        break;
                    case "date":
                        orderBy = (sort == "Asc") ? q => q.OrderBy(order => order.OrderDate) : q => q.OrderByDescending(order => order.OrderDate);
                        break;
                    default:
                        orderBy = (sort == "Asc") ? q => q.OrderBy(order => order.Id) : q => q.OrderByDescending(order => order.Id);
                        break;
                }

                var orders = await unitOfWork.Orders.GetAll(expression,
                    orderBy,
                    new List<string> { "OrderDetails"},
                    new PaginationFilter(pageNumber, pageSize));
                var count = await unitOfWork.Orders.GetCount(expression);
                var result = mapper.Map<IList<OrderDTO>>(orders);
                return Ok(new { success = true, result = result, totalOrder = count });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreateUserForAdmin([FromBody] UserRegisterDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new { error = "Dữ liệu chưa hợp lệ", success = false });
            }
            var existUser = await userManager.FindByNameAsync(dto.UserName);
            if (existUser!=null)
            {
                return Ok(new { error = "Đã tồn tại người dùng với username này!", success = false });

            }
            existUser = await userManager.FindByEmailAsync(dto.Email);
            if (existUser!=null)
            {
                return Ok(new { error = "Đã tồn tại người dùng với email này!", success = false });
            }
            try
            {
                var user = mapper.Map<AppUser>(dto);
                user.EmailConfirmed = true;
                var result = await userManager.CreateAsync(user, dto.Password);
                var errors = new List<string>();
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        errors.Add(error.Code);
                    }
                    return Ok(new { errors = errors, success = false });
                }
                await userManager.AddToRolesAsync(user, dto.Roles);

                return Ok(new { success = true, user });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDTO dto, string id)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new { error = "Dữ liệu chưa hợp lệ", success = false });
            }
            try
            {
                var user = await unitOfWork.Users.Get(q => q.Id == id);
                if (user == null)
                {
                    return Ok(new { error = "Không tìm thấy dữ liệu", success = false });
                }
                mapper.Map(dto, user);
                unitOfWork.Users.Update(user);
                await unitOfWork.Save();

                return Ok(new { user = mapper.Map<SimpleUserForAdminDTO>(user), success = true });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await unitOfWork.Users.Get(q => q.Id == id);
            if (user == null)
            {
                return Ok(new { success = false, msg = "Không tìm thấy user!" });
            }
            await userManager.DeleteAsync(user);
            await unitOfWork.Save();
            return Ok(new { success = true });
        }
    }
}
