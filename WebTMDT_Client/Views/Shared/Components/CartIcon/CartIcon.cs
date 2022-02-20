using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebTMDTLibrary.DTO;

namespace WebTMDT_Client.Views.Shared.Components.CartIcon
{
    [ViewComponent]
    public class CartIcon : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var session = HttpContext.Session;

            var cart_str = session.GetString("cart");
            if (cart_str == null)
            {
             
                ViewBag.TotalItem = 0;
            }
            else
            {
                Cart cart = JsonConvert.DeserializeObject<Cart>(cart_str);
                ViewBag.TotalItem = cart.TotalItem;
            }
            return View("CartIcon");
        }
    }
}
