using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebTMDT_Client.Service;
using WebTMDTLibrary.DTO;

namespace WebTMDT_Client.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;
        public OrderController(IOrderService _orderService)
        {
            this.orderService = _orderService;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult Checkout()
        {
            var session = HttpContext.Session;
            var cart_str = session.GetString("cart");

            if (cart_str != null)
            {
                Cart cart = JsonConvert.DeserializeObject<Cart>(cart_str);
                if (cart.TotalItem == 0)
                {
                    return RedirectToAction("Index", "Cart");
                }
                ViewBag.TotalPrice = cart.TotalPrice;
                ViewBag.TotalItem = cart.TotalItem;
            }
            else
            {
                return RedirectToAction("Index", "Cart");
            }


            var user_string = HttpContext.Session.GetString("User");
            if (user_string == null)
            {
                ViewBag.Login = false;
            }
            else
            {
                ViewBag.Login = true;
            }


            Dictionary<string, string> dic = new Dictionary<string, string>();
            return View(dic);
        }

        [HttpPost]
        public IActionResult Checkout([FromForm] PostOrderDTO dto)
        {
            var session = HttpContext.Session;
            var token = session.GetString("Token");
            SimpleUserDTO user = JsonConvert.DeserializeObject<SimpleUserDTO>(session.GetString("User"));
            var cart_str = session.GetString("cart");
            Cart cart = null;

            if (cart_str != null)
            {
                cart = JsonConvert.DeserializeObject<Cart>(cart_str);
                if (cart.TotalItem == 0)
                {
                    return RedirectToAction("Index", "Cart");
                }
        
            }
            else
            {
                return RedirectToAction("Index", "Cart");
            }

            if (!ModelState.IsValid)
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                foreach (var modelStateKey in ModelState.Keys)
                {
                    var modelStateVal = ModelState[modelStateKey];
                    foreach (var error in modelStateVal.Errors)
                    {
                        var key = modelStateKey;
                        var errorMessage = error.ErrorMessage;
                        //var exception = error.Exception;
                        dic.Add(key, errorMessage);
                    }
                }
                foreach (var item in dic)
                {
                    Console.WriteLine(item.Key + " : " + item.Value);
                }
                ViewBag.TotalPrice = cart.TotalPrice;
                ViewBag.TotalItem = cart.TotalItem;

                var user_string = HttpContext.Session.GetString("User");
                if (user_string == null)
                {
                    ViewBag.Login = false;
                }
                else
                {
                    ViewBag.Login = true;
                }
                return View(dic);
            }

            try
            {
                session.SetString("Ordering", "true");
                if (dto.PaymentMethod == "vnpay")
                {
                    var paymentUrl = orderService.GetVNPayUrl(cart.TotalPrice,token);
                    session.SetString("VNPAY", "true");
                    session.SetString("VNPAY_Order", JsonConvert.SerializeObject(dto));
                    return Redirect(paymentUrl);
                }
                else
                {
                    var res = orderService.GetPostOrderResponse(dto, cart, token, user.Id);
                    if (res.success)
                    {
                        Cart newCart = new Cart();
                        session.SetString("cart",JsonConvert.SerializeObject(newCart));
                        return RedirectToAction("Thankyou", "Order");
                    }
                    else
                    {
                        return RedirectToAction("Checkout", "Order");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction("Checkout", "Order");
            }
        }

        public IActionResult Thankyou(string vnp_ResponseCode,string vnp_SecureHash,string vnp_TxnRef,string vnp_TransactionStatus,string vnp_TransactionNo,string vnp_TmnCode,string vnp_PayDate,string vnp_OrderInfo,string vnp_CardType,string vnp_BankTranNo,string vnp_BankCode,string vnp_Amount)
        {
            var session = HttpContext.Session;
            var token = session.GetString("Token");
            SimpleUserDTO user = JsonConvert.DeserializeObject<SimpleUserDTO>(session.GetString("User"));
            var cart_str = session.GetString("cart");
            Cart cart = null;
            if (cart_str != null)
            {
                cart = JsonConvert.DeserializeObject<Cart>(cart_str);
                if (cart.TotalItem == 0)
                {
                    return RedirectToAction("Index", "Cart");
                }
            }
            else
            {
                return RedirectToAction("Index", "Cart");
            }
            ViewBag.TotalPrice = cart.TotalPrice;
            ViewBag.TotalItem = cart.TotalItem;
            var user_string = session.GetString("User");
            if (user_string == null)
            {
                ViewBag.Login = false;
            }
            else
            {
                ViewBag.Login = true;
            }

            var ordering = HttpContext.Session.GetString("Ordering");
            if (ordering!=null)
            {
                HttpContext.Session.Remove("Ordering");
                if (!vnp_ResponseCode.Equals("00"))
                {

                    return RedirectToAction("Checkout", "Order");
                }
                else
                {
                 
                    var order = JsonConvert.DeserializeObject<PostOrderDTO>(HttpContext.Session.GetString("VNPAY_Order"));
                    var res = orderService.GetPostOrderResponse(order, cart, token, user.Id);
                    if (res.success)
                    {
                        Cart newCart = new Cart();
                        session.SetString("cart", JsonConvert.SerializeObject(newCart));
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("Checkout", "Order");
                    }
                }

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }


}
