using Microsoft.AspNetCore.Mvc;
using WebTMDT_Client.Service;
using WebTMDTLibrary.DTO;

namespace WebTMDT_Client.Views.Shared.Components.GenreList
{
    [ViewComponent]
    public class GenreList : ViewComponent
    {
        private readonly IGenreService genreService;
        public GenreList(IGenreService _genreService)
        {
            genreService = _genreService;
        }
        public IViewComponentResult Invoke()
        {
            List<GenreDTO> genres = genreService.GetGenres();

            return View("GenreList",genres);
        }
    }
}
