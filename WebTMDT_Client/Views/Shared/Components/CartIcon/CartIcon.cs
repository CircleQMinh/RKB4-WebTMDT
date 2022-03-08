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
            var total = 0;

            var cart_str = session.GetString("cart");
            if (cart_str == null)
            {
             
               total= 0;
            }
            else
            {
                WebTMDTLibrary.DTO.Cart cart = JsonConvert.DeserializeObject<WebTMDTLibrary.DTO.Cart>(cart_str);
                total= cart.TotalItem;
            }
            return View("CartIcon",total);
        }
    }
}
