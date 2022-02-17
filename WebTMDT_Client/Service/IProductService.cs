using WebTMDT_Client.ViewModel;

namespace WebTMDT_Client.Service
{
    public interface IProductService
    {
        public ProductViewViewModel GetProductViewViewModel(ProductListFilterModel model);
        public ProductListViewModel GetProductListViewModel(ProductListFilterModel model);
    }
}
