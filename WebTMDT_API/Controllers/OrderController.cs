using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using WebTMDT_API.Repository;
using WebTMDT_API.Data;
using WebTMDTLibrary.DTO;
using WebTMDT_API.Helper;
using WebTMDT_API.Hepler;

namespace WebTMDT_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public OrderController(IUnitOfWork _unitOfWork, IMapper _mapper,IConfiguration _configuration)
        {
            configuration = _configuration;
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }
        [HttpGet("getVNPayUrl", Name = "getVNPayUrl")]
        [Authorize]
        public async Task<IActionResult> GetVNPayUrl(int totalPrice)
        {
            try
            {
                //Get Config Info
                string vnp_Returnurl = "https://localhost:7094/Order/Thankyou";//URL nhan ket qua tra ve 
                string vnp_Url = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html"; //URL thanh toan cua VNPAY 
                string vnp_TmnCode = "K3IS060E"; //Ma website
                string vnp_HashSecret = "TPNMDBCUDPXMJCVFZTSYEKWXPAQHFFPW";//Chuoi bi mat


                //Get payment input
                VNPayOrderInfo order = new VNPayOrderInfo();
                //Save order to db
                order.OrderId = DateTime.Now.Ticks; // Giả lập mã giao dịch hệ thống merchant gửi sang VNPAY
                order.Amount = totalPrice; // Giả lập số tiền thanh toán hệ thống merchant gửi sang VNPAY 100,000 VND
                order.Status = "0"; //0: Trạng thái thanh toán "chờ thanh toán" hoặc "Pending"
                order.OrderDesc = "Thanh toan don hang cho CircleShop";
                order.CreatedDate = DateTime.Now;
                string locale = "vn";
                //Build URL for VNPAY
                VnPayLibrary vnpay = new VnPayLibrary();

                vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
                vnpay.AddRequestData("vnp_Command", "pay");
                vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
                vnpay.AddRequestData("vnp_Amount", (order.Amount * 100).ToString()); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000
                vnpay.AddRequestData("vnp_BankCode", ""); //Mã Ngân hàng thanh toán (tham khảo: https://sandbox.vnpayment.vn/apis/danh-sach-ngan-hang/), có thể để trống, người dùng có thể chọn trên cổng thanh toán VNPAY
                vnpay.AddRequestData("vnp_CreateDate", order.CreatedDate.ToString("yyyyMMddHHmmss"));
                vnpay.AddRequestData("vnp_CurrCode", "VND");
                vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress());
                if (!string.IsNullOrEmpty(locale))
                {
                    vnpay.AddRequestData("vnp_Locale", locale);
                }
                else
                {
                    vnpay.AddRequestData("vnp_Locale", "vn");
                }
                vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + order.OrderId);
                vnpay.AddRequestData("vnp_OrderType", "Other"); //default value: other
                vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
                vnpay.AddRequestData("vnp_TxnRef", DateTime.Now.Ticks.ToString()); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày

                //Add Params of 2.1.0 Version
                vnpay.AddRequestData("vnp_ExpireDate", "20221003135123");


                string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);

                return Ok(new { paymentUrl });

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }


        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetOrderById(int id)
        {
            try
            {
                var order = await unitOfWork.Orders.Get(q => q.Id == id, new List<string> { "OrderDetails", "DiscountCode", "Shipper" });
                if (order == null)
                {
                    return Ok(new { success = false, msg = "Không tìm thấy đơn hàng." });
                }
                var result = mapper.Map<OrderDTO>(order);
                return Ok(new { success = true, result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id}/orderdetails")]
        [Authorize]
        public async Task<IActionResult> GetAllOrderDetailByOrderId(int id)
        {
            try
            {
                var orderDetails = await unitOfWork.OrderDetails.GetAll(q => q.OrderId == id, null, new List<string> { "Book" });
                if (orderDetails.Count == 0)
                {
                    return Ok(new { success = false, msg = "Không tìm thấy đơn hàng." });
                }
                var result = mapper.Map<IList<OrderDetailDTO>>(orderDetails);
                return Ok(new { success = true, result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("history/{id}")]
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
                    new List<string> { "OrderDetails", "DiscountCode", "Shipper" },
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
        [Authorize]
        public async Task<IActionResult> PostOrder([FromBody] CreateOrderDTO order)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new { error = "Dữ liệu chưa hợp lệ", success = false });
            }
            try
            {
                //lưu OK
                var newOrder = mapper.Map<Order>(order);
                await unitOfWork.Orders.Insert(newOrder);
                await unitOfWork.Save();
                // lấy thông tin để gửi email
                var orderInfo = await unitOfWork.Orders.Get(q => q.Id == newOrder.Id, new List<string> { "OrderDetails", "DiscountCode", "Shipper" });

                var updateOrderDetailsList = new List<OrderDetail>();
                foreach (var od in orderInfo.OrderDetails)
                {
                    var item = await unitOfWork.OrderDetails.Get(q => q.Id == od.Id, new List<string> { "Book" });
                    updateOrderDetailsList.Add(item);
                }
                orderInfo.OrderDetails = updateOrderDetailsList;

                //mapping rồi gửi
                var result = mapper.Map<OrderDTO>(orderInfo);
                EmailHelper emailHelper = new EmailHelper(configuration);
                string emailResponse = emailHelper.SendEmailWithOrderInfo(order.Email, result, result.OrderDetails);

                return Ok(new { order = newOrder, success = true });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutOrder([FromBody] EditOrderDTO dto, int id)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new { error = "Dữ liệu chưa hợp lệ", success = false });
            }
            try
            {
                var order = await unitOfWork.Orders.Get(q => q.Id == id);
                if (order == null)
                {
                    return Ok(new { error = "Không tìm thấy đơn hàng", success = false });
                }
                order.Note = dto.Note;
                order.Status = dto.Status;

                unitOfWork.Orders.Update(order);
                await unitOfWork.Save();

                return Ok(new { order = order, success = true });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            try
            {
                var order = await unitOfWork.Orders.Get(q => q.Id == id);
                if (order == null)
                {
                    return Ok(new { success = false, msg = "Không tìm thấy đơn hàng" });
                }
                await unitOfWork.Orders.Delete(order.Id);
                await unitOfWork.Save();
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.ToString() });
            }
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetOrders(string status, string orderby, string sort, int pageNumber, int pageSize)
        {

            try
            {
                Expression<Func<Order, bool>> expression = status == "all" ? q => true : q => q.Status == int.Parse(status);
                Func<IQueryable<Order>, IOrderedQueryable<Order>> orderBy = null;
                switch (orderby)
                {
                    case "Id":
                        orderBy = (sort == "Asc") ? q => q.OrderBy(order => order.Id) : q => q.OrderByDescending(order => order.Id);
                        break;
                    case "totalPrice":
                        orderBy = (sort == "Asc") ? q => q.OrderBy(order => order.TotalPrice) : q => q.OrderByDescending(order => order.TotalPrice);
                        break;
                    case "contactName":
                        orderBy = (sort == "Asc") ? q => q.OrderBy(order => order.ContactName) : q => q.OrderByDescending(order => order.ContactName);
                        break;
                    case "orderDate":
                        orderBy = (sort == "Asc") ? q => q.OrderBy(order => order.OrderDate) : q => q.OrderByDescending(order => order.OrderDate);
                        break;
                    default:
                        orderBy = (sort == "Asc") ? q => q.OrderBy(order => order.Id) : q => q.OrderByDescending(order => order.Id);
                        break;
                }

                var orders = await unitOfWork.Orders.GetAll(expression, orderBy, new List<string> { "OrderDetails", "Shipper", "DiscountCode" }, new PaginationFilter(pageNumber, pageSize));
                var count = await unitOfWork.Orders.GetCount(expression);
                var result = mapper.Map<IList<OrderDTO>>(orders);



                return Accepted(new { result = result, totalOrder = count });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }

        }

    }
}
