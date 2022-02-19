using WebTMDTLibrary.DTO;

namespace WebTMDT_Client.ResponseModel
{
    public class ProductListViewModel
    {
        public List<BookDTO> result { get; set; }
        public int totalPage { get; set; }
    }
}
