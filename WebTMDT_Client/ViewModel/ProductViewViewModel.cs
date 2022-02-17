using WebTMDT_Client.DTO;

namespace WebTMDT_Client.ViewModel
{
    public class ProductViewViewModel
    {
        public GenresDeserialize Genres { get; set; }
        public BooksDeserialize Books { get; set; }

        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        
    }
}
