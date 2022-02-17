namespace WebTMDT.Data
{
    public class Review
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int Star { get; set; }
        public string Content { get; set; }
        public bool Recomended { get; set; }
        public DateTime Date { get; set; }
        public string UserID { get; set; }
        public virtual AppUser User { get; set; }
    }
}
