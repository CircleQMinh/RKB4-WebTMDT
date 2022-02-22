using Microsoft.AspNetCore.Mvc;
using WebTMDTLibrary.DTO;

namespace WebTMDT_Client.Controllers
{
    public class ReviewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PostReview(PostReviewDTO model)
        {

            Console.WriteLine(model.Content);
            Console.WriteLine(model.Star);
            Console.WriteLine(model.Recomended);
            Console.WriteLine(model.UserID);
            return RedirectToAction("Index","Product",model.BookId);
        }
    }
}
