using WebTMDT_Client.ResponseModel;
using WebTMDT_Client.ViewModel;

namespace WebTMDT_Client.Service
{
    public interface IProductService
    {
        public Task<ProductViewViewModel> GetProductViewViewModel(ProductListFilterModel model);
        public Task<ProductListViewModel> GetProductListViewModel(ProductListFilterModel model);
        public Task<ProductDetailViewModel> GetProductDetailViewModel(int id);

        public Task<ProductDisplayViewModel> GetPoppularProduct();
        public Task<ProductDisplayViewModel> GetSuggestProduct();
        public Task<ProductDisplayViewModel> GetLatestProduct();

        public Task<ProductDisplayViewModel> GetRelatedProduct(int id);
    }
}
