using Microsoft.AspNetCore.Mvc;
using WebTMDTLibrary.DTO;

namespace WebTMDT_Client.Views.Shared.Components.Comment
{
    [ViewComponent]
    public class Comment : ViewComponent
    {
        public IViewComponentResult Invoke(List<ReviewDTO> reviews)
        {

            return View("Comment",reviews);
        }
    }
}
