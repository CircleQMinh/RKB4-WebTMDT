using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebTMDTLibrary.DTO;

namespace WebTMDT_Client.Views.Shared.Components.LoginInfo
{
    [ViewComponent]
    public class LoginInfo : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            SimpleUserDTO user = null;
            var user_string = HttpContext.Session.GetString("User");
            if (user_string!=null)
            {
                user = JsonConvert.DeserializeObject<SimpleUserDTO>(user_string);
                ViewBag.username = user.UserName;
            }

            return View("LoginInfo");
        }
    }
}
