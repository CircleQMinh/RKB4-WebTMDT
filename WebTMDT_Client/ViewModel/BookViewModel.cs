using WebTMDT_Client.DTO;

namespace WebTMDT_Client.ViewModel
{
    public class BookViewModel
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
}
