namespace WebTMDT_Client.Data
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string imgUrl { get; set; }
        public double Price { get; set; }
        public int PublishYear { get; set; }
        public int NumberOfPage { get; set; }
        public virtual IList<Genre> Genres { get; set; }
        public virtual IList<Author> Authors { get; set; }
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }
        public virtual IList<Review> Reviews { get; set; }
        public int? PromotionInfoID { get; set; }
        public PromotionInfo? PromotionInfo { get; set; }

        public virtual IList<AppUser> WishlistUsers { get; set; }

        public Book()
        {
            Genres = new List<Genre>();
            Authors = new List<Author>(); 
            Reviews = new List<Review>();
            WishlistUsers = new List<AppUser>();
        }
    }
}
