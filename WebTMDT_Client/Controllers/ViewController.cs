using Microsoft.AspNetCore.Mvc;
using WebTMDT_Client.ResponseModel;

namespace WebTMDT_Client.Controllers
{
    public class ViewController : Controller
    {
        public IActionResult GetProductList(int pageNumber, int pageSize, string? keyword = null, string? priceRange = null, string? genreFilter = null)
        {
            Console.WriteLine(pageNumber);
            Console.WriteLine(pageSize);
            Console.WriteLine(keyword);
            Console.WriteLine(genreFilter);
            Console.WriteLine(priceRange);
            return ViewComponent("ProductList",new ProductListFilterModel() {pageNumber=pageNumber,pageSize=pageSize,keyword=keyword,priceFilter=priceRange,genreFilter=genreFilter });
        }
    }
}
