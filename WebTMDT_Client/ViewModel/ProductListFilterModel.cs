using System.ComponentModel.DataAnnotations;

namespace WebTMDT_Client.ViewModel
{
    public class ProductListFilterModel
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public string? genreFilter { get; set; }
        public string? priceFilter { get; set; }
        public string? keyword { get; set; }

        public ProductListFilterModel()
        {
            pageNumber = 0;
            pageSize = 0;
        }
    }
}
