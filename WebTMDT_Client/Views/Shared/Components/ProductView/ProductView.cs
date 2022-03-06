using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebTMDTLibrary.DTO;
using WebTMDT_Client.Service;
using WebTMDT_Client.ResponseModel;
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
        public async Task<IViewComponentResult> InvokeAsync(ProductListFilterModel model)
        {
            ProductViewViewModel p_model = await productService.GetProductViewViewModel(model);
            return View("ProductView", p_model);
        }
    }
}
