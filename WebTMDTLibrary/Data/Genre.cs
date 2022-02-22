namespace WebTMDTLibrary.Data
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual IList<Book> Books { get; set; }

        public Genre()
        {
            Books = new List<Book>();
        }
    }
}
