namespace WebTMDT_API.Data
{
    public class Publisher
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual IList<Book> Books { get; set; }
        public Publisher()
        {
            Books = new List<Book>();
        }

    }
}
