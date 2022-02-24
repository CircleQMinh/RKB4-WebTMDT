using Microsoft.AspNetCore.Mvc;
using WebTMDTLibrary.DTO;

namespace WebTMDT_Client.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Checkout()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            return View(dic);
        }

        [HttpPost]
        public IActionResult Checkout([FromForm]PostOrderDTO dto)
        {
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
                return View(dic);
            }
            return Content("Send order");

        }

    }
}
