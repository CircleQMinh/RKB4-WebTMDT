using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebTMDTLibrary.DTO;

namespace WebTMDT_Client.Controllers
{
    public class CartController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var session = HttpContext.Session;
            var cart_str = session.GetString("cart");
            if (cart_str != null)
            {
                Cart cart = JsonConvert.DeserializeObject<Cart>(cart_str);
                return View("Cart", cart);
            }
            else
            {
                Cart cart = new Cart();
                return View("Cart", cart);
            }
        }
        [HttpPost]
        public IActionResult AddToCart([FromBody] CartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                var session = HttpContext.Session;
                var cart_str = session.GetString("cart");
                Cart cart = new Cart();
                if (cart_str != null)
                {
                    cart = JsonConvert.DeserializeObject<Cart>(cart_str);
                    cart = CartHelper.AddCartItem(cartItem, cart);
                    cart = CartHelper.CalculateCartTotal(cart.Items);
                    session.SetString("cart", JsonConvert.SerializeObject(cart));
                    Console.WriteLine("tìm thấy cart cũ");
                }
                else
                {
                 
                    cart = CartHelper.AddCartItem(cartItem, cart);
                    cart = CartHelper.CalculateCartTotal(cart.Items);
                    session.SetString("cart", JsonConvert.SerializeObject(cart));
                    Console.WriteLine("ko thấy cart cũ");
                }

                return Accepted(new { success = true,cart });
            }
            return Accepted(new { success = false });
        }
        [HttpPost]
        public IActionResult RemoveFromCart([FromBody] CartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                var session = HttpContext.Session;
                var cart_str = session.GetString("cart");
                if (cart_str == null)
                {
                    return Accepted(new { success = false,message = "Không tìm thấy cart!" });
                }
                Cart cart = JsonConvert.DeserializeObject<Cart>(cart_str);
                cart = CartHelper.RemoveCartItem(cartItem, cart);
                cart = CartHelper.CalculateCartTotal(cart.Items);
                session.SetString("cart", JsonConvert.SerializeObject(cart));
                return Accepted(new { success = true });
            }
            return Accepted(new { success = false });
        }

        [HttpDelete]
        public IActionResult DeleteFromCart([FromBody] CartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                var session = HttpContext.Session;
                var cart_str = session.GetString("cart");
                if (cart_str == null)
                {
                    return Accepted(new { success = false, message = "Không tìm thấy cart!" });
                }
                Cart cart = JsonConvert.DeserializeObject<Cart>(cart_str);
                cart = CartHelper.DeleteCartItem(cartItem, cart);
                cart = CartHelper.CalculateCartTotal(cart.Items);
                session.SetString("cart", JsonConvert.SerializeObject(cart));
                return Accepted(new { success = true });
            }
            return Accepted(new { success = false });
        }
    }
}
