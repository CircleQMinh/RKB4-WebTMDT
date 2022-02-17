using WebTMDT_Client.DTO;

namespace WebTMDT_Client.ViewModel
{
    public class ProductListViewModel
    {
        public List<BookDTO> result { get; set; }
        public int totalPage { get; set; }
    }
}
