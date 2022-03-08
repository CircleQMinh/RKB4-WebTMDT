using Microsoft.AspNetCore.Mvc;
using WebTMDT_Client.Service;

namespace WebTMDT_Client.Views.Shared.Components.ProductPopular
{
    [ViewComponent]
    public class ProductPopular:ViewComponent
    {
        private readonly IProductService productService;
        public ProductPopular(IProductService _productService)
        {
            this.productService = _productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await productService.GetPoppularProduct();
            return View("ProductPopular",model); 
        }
    }
}
