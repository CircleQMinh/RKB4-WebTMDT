namespace WebTMDT_Client.DTO
{
    public class ReviewDTO //
    {
        public int BookId { get; set; }
        public int Star { get; set; }
        public string Content { get; set; }
        public bool Recomended { get; set; }
        public DateTime Date { get; set; }
        public string UserID { get; set; }
        public virtual SimpleUserDTO User { get; set; }
    }
    public class CreateReviewDTO
    {
        public int BookId { get; set; }
        public int Star { get; set; }
        public string Content { get; set; }
        public bool Recomended { get; set; }
        public DateTime Date { get; set; }
        public string UserID { get; set; }
    }
    public class DeleteReviewDTO
    {
        public int BookId { get; set;}
        public string UserID { get; set; }
    }
}
