using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebTMDT_Client.DTO;
using WebTMDT_Client.Service;
using WebTMDT_Client.ViewModel;

namespace WebTMDT_Client.Views.Shared.Components.ProductView
{
    [ViewComponent]
    public class ProductView : ViewComponent
    {

        private readonly IProductService productService;
        public ProductView(IProductService _productService)
        {
            productService = _productService;
        }
        public IViewComponentResult Invoke(ProductListFilterModel model)
        {
            ProductViewViewModel p_model = productService.GetProductViewViewModel(model);
            return View("ProductView", p_model);
        }
    }
}
