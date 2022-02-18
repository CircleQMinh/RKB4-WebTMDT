
using WebTMDTLibrary.DTO;

namespace WebTMDTLibrary.DTO
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string imgUrl { get; set; }
        public double Price { get; set; }
        public int PublishYear { get; set; }
        public int? NumberOfPage { get; set; }
        public virtual IList<GenreDTO> Genres { get; set; }
        public virtual IList<AuthorDTO> Authors { get; set; }
        public int PublisherId { get; set; }
        public PublisherDTO Publisher { get; set; }
        public int? PromotionInfoID { get; set; }
        public PromotionInfoDTO PromotionInfo { get; set; }
    }

    public class SimpleBookInfoDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string imgUrl { get; set; }
        public double Price { get; set; }
        public int PublishYear { get; set; }
        public int NumberOfPage { get; set; }
    }


    public class CreateBookDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string imgUrl { get; set; }
        public double Price { get; set; }
        public int PublishYear { get; set; }
        public int NumberOfPage { get; set; }
        public  IList<string> Genres { get; set; }
        public  IList<string> Authors { get; set; }
        public string PublisherName { get; set; }
  
    }

    public class PopularBookDTO
    {
        public BookDTO Book { get; set; }
        public int Sales    { get; set; }
    }

    public class AddBookToWishlist
    {
        public string UserId { get; set; }
        public int BookId { get; set; }
    }
    public class BooksDeserialize
    {
        public List<BookDTO> result { get; set; }
        public int totalPage { get; set; }
    }
}
