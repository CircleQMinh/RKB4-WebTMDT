using WebTMDT_Client.ResponseModel;

namespace WebTMDT_Client.Service
{
    public interface IProductService
    {
        public ProductViewViewModel GetProductViewViewModel(ProductListFilterModel model);
        public ProductListViewModel GetProductListViewModel(ProductListFilterModel model);
    }
}
