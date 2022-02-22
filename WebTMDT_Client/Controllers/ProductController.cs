using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebTMDT_Client.Service;
using WebTMDT_Client.ViewModel;
using WebTMDTLibrary.DTO;

namespace WebTMDT_Client.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService productService;

        public ProductController(ILogger<ProductController> logger,IProductService _productService)
        {
            _logger = logger;
            productService = _productService;
        }
        public IActionResult Index(int id)
        {
            ProductDetailViewModel model = productService.GetProductDetailViewModel(id);
            SimpleUserDTO user = null;
            var user_string = HttpContext.Session.GetString("User");
            if (user_string != null)
            {
                user = JsonConvert.DeserializeObject<SimpleUserDTO>(user_string);
                ViewBag.UserId = user.Id;
                ViewBag.Token = HttpContext.Session.GetString("Token");
            }
            return View("ProductDetail",model);
        }


    }
}
