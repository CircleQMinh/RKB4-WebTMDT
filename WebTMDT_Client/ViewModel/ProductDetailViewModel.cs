using WebTMDTLibrary.DTO;

namespace WebTMDT_Client.ViewModel
{
    public class ProductDetailViewModel
    {
        public BookDTO result { get; set; }
        public List<ReviewDTO> reviews { get; set; }
        public bool success { get; set; }
    }
}
