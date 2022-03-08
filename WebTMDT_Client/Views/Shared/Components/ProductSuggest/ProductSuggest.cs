using Microsoft.AspNetCore.Mvc;
using WebTMDT_Client.Service;

namespace WebTMDT_Client.Views.Shared.Components.ProductSuggest
{
    [ViewComponent]
    public class ProductSuggest:ViewComponent
    {
        private readonly IProductService productService;
        public ProductSuggest(IProductService _productService)
        {
            this.productService = _productService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await productService.GetSuggestProduct();

            return View("ProductSuggest", model);
        }
    }
}
