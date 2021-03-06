using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebTMDT_Client.Service;
using WebTMDT_Client.ViewModel;
using WebTMDTLibrary.DTO;

namespace WebTMDT_Client.Pages
{
    public class CategoryModel : PageModel
    {
        private readonly IProductService productService;
        private readonly IGenreService genreService;
        public List<BookDTO> books { get; set; }
        public int currentPage { get; set; }
        public int totalPage { get; set; }
        public GenreInfoDTO genre { get; set; }
        public CategoryModel(IProductService _productService, IGenreService _genreService)
        {
            this.productService = _productService;
            this.genreService = _genreService;
        }
        public async Task<IActionResult> OnGet()
        {
            var id = Request.RouteValues["id"];
            var pageNumber = Request.Query["pageNumber"];
            if (id == null)
            {
                Console.WriteLine("Error");
            }
            else
            {
                if (pageNumber == Microsoft.Extensions.Primitives.StringValues.Empty)
                {
                    return Redirect($"/Genre/{id}?pageNumber=1");
                }
                try
                {

                    genre = await genreService.GetGenre(Int32.Parse(id.ToString()));
                    if (genre==null)
                    {
                        return Redirect("/error");
                    }
                    var model = await productService.GetProductListViewModel(
                    new ProductListFilterModel()
                    {
                        genreFilter = genre.Name,
                        pageNumber = Int32.Parse(pageNumber.ToString()),
                        pageSize = 8
                    });
                    books = model.result;
                    totalPage = model.totalPage;
                    currentPage = Int32.Parse(pageNumber);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return Redirect("/error");
                }

            }
            return null;
        }
    }
}
