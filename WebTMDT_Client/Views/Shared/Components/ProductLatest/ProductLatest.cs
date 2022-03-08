using Microsoft.AspNetCore.Mvc;
using WebTMDT_Client.Service;

namespace WebTMDT_Client.Views.Shared.Components.ProductLastest
{
    [ViewComponent]
    public class ProductLatest:ViewComponent
    {
        private readonly IProductService productService;
        public ProductLatest(IProductService _productService)
        {
            this.productService = _productService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            var model = await productService.GetLatestProduct();
            return View("ProductLatest", model);
        }
    }
}
