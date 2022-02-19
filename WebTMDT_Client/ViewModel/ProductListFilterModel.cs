namespace WebTMDT_Client.ResponseModel
{
    public class ProductListFilterModel
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public string genreFilter { get; set; }
        public string priceFilter { get; set; }
        public string keyword { get; set; }
    }
}
