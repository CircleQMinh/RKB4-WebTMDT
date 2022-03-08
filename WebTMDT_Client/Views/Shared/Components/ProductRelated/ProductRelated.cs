using Microsoft.AspNetCore.Mvc;
using WebTMDT_Client.Service;

namespace WebTMDT_Client.Views.Shared.Components.ProductRelated
{
    [ViewComponent]
    public class ProductRelated : ViewComponent
    {
        private readonly IProductService productService;
        public ProductRelated(IProductService _productService)
        {
            this.productService = _productService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var model = await productService.GetRelatedProduct(id);
            return View("ProductRelated",model); 
        }
    }
}
