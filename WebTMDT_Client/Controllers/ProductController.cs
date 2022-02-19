using Microsoft.AspNetCore.Mvc;

namespace WebTMDT_Client.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }
        public IActionResult ProductDetail(int id)
        {
            Console.WriteLine(id);
            var token = HttpContext.Session.GetString("token");
            Console.Write(token);
            return View();
        }
    }
}
