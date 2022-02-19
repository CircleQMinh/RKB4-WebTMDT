using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WebTMDT_Client.Service;
using WebTMDT_Client.ResponseModel;
using WebTMDTLibrary.DTO;
using WebTMDTLibrary.Hepler;

namespace WebTMDT_Client.Controllers
{
    public class AccountController : Controller
    {

        private readonly IAccountService accountService;
        public AccountController(IAccountService _accountService)
        {
            accountService = _accountService;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Login(LoginUserDTO model)
        {
            string message = model.Email+" "+model.Password;
            var login_response = new LoginResponseModel();

            if (ModelState.IsValid)
            {
                login_response = accountService.Login(model);
                if (login_response.success)
                {
                    HttpContext.Session.SetString("Login", "true");
                    HttpContext.Session.SetString("Token", login_response.token);
                    HttpContext.Session.SetString("User", JsonConvert.SerializeObject(login_response.user));
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View("Login", "Email hoặc password không chính xác!");
                }
            }
            else
            {
                return View("Login", "Email hoặc password không chính xác!");
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterDTO dto)
        {
            var imgurl = ProjectConst.Default_IMGURL;
            if (ModelState.IsValid)
            {
                UserRegisterDTO user_dto = new UserRegisterDTO() 
                { Email=dto.Email,UserName=dto.UserName,imgUrl=imgurl,Password=dto.Password,PhoneNumber=dto.PhoneNumber,Roles = new List<string> {"User"} };
                var register_response = accountService.Register(user_dto);
                if (register_response)
                {
                    ViewBag.DoneRegister = true;
                    return View();
                }
                else
                {
                    return View("Register", "Có lỗi xảy ra!");
                }
            }
            else
            {
                return View("Register", "Thông tin nhập không chính xác!");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Confirm(string email,string token)
        {
            ConfirmEmailResponseModel model = accountService.ConfirmEmail(new ConfirmEmailDTO { email=email, token=token });
            if (model.success)
            {
                return View("Confirm", "Xác thực thành công!");
            }
            else
            {
                return View("Confirm",model.message);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
