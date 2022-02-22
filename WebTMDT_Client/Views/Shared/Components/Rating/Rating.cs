using Microsoft.AspNetCore.Mvc;
using WebTMDT_Client.ViewModel;

namespace WebTMDT_Client.Views.Shared.Components.Rating
{
    [ViewComponent]
    public class Rating : ViewComponent
    {
        public IViewComponentResult Invoke(RatingViewModel model)
        {
            return View("Rating",model);
        }
    }
}
