using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebTMDT_Client.Views.Shared.Components.Cart
{
    [ViewComponent]
    public class Cart:ViewComponent
    {
        public IViewComponentResult Invoke()
        {

            var session = HttpContext.Session;
            var cart_str = session.GetString("cart");
            if (cart_str != null)
            {
                WebTMDTLibrary.DTO.Cart cart = JsonConvert.DeserializeObject<WebTMDTLibrary.DTO.Cart>(cart_str);
                return View("Cart", cart);
            }
            else
            {
                WebTMDTLibrary.DTO.Cart cart = new WebTMDTLibrary.DTO.Cart();
                return View("Cart", cart);
            }
        }
    }
}
