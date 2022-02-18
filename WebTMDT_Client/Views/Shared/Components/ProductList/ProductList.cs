
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebTMDTLibrary.DTO;
using WebTMDT_Client.Service;
using WebTMDT_Client.ViewModel;

namespace WebTMDT.Views.Shared.Components.ProductList
{
    [ViewComponent]
    public class ProductList : ViewComponent
    {
        private readonly IProductService productService;
        public ProductList(IProductService _productService)
        {
            productService = _productService;
        }
        public IViewComponentResult Invoke(ProductListFilterModel model)
        {
            ProductListViewModel books = productService.GetProductListViewModel(model);
            ViewBag.pageNumber = model.pageNumber;
            ViewBag.pageSize = model.pageSize;
           

            return View("ProductList", books);
        }

    }
}
