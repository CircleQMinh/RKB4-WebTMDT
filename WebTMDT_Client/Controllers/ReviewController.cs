using Microsoft.AspNetCore.Mvc;
using WebTMDT_Client.ResponseModel;
using WebTMDT_Client.Service;
using WebTMDTLibrary.DTO;
using WebTMDTLibrary.Hepler;

namespace WebTMDT_Client.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService reviewService;
        public IActionResult Index()
        {
            return View();
        }
        public ReviewController(IReviewService _reviewService)
        {
            this.reviewService = _reviewService;
        }

        [HttpPost]
        public IActionResult PostReview(PostReviewDTO model)
        {
            var token = HttpContext.Session.GetString("Token");
            PostReviewResponseModel response = reviewService.GetPostReviewResponse(model,token);
            if (response.success)
            {
                if (response.newReview)
                {
                    HttpContext.Session.SetString("reviewPostResult", "1");
                }
                if (response.update)
                {
                    HttpContext.Session.SetString("reviewPostResult", "2");
                }
            }
            else
            {
                HttpContext.Session.SetString("reviewPostResult", "0");
            }
            return RedirectToAction("Index","Product", new { id = model.BookId });
        }

        [HttpPost]
        public IActionResult DeleteReview(DeleteReviewDTO dto)
        {
            var token = HttpContext.Session.GetString("Token");
            DeleteReviewResponse response =  reviewService.GetDeleteReviewResponse(dto,token);
            if (response.success)
            {
                HttpContext.Session.SetString("reviewDeleteResult", "1");
            }
            else
            {
                HttpContext.Session.SetString("reviewDeleteResult", "0");
            }
            return RedirectToAction("Index", "Product", new { id = dto.BookId });
        }
    }
}
