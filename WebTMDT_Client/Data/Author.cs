﻿namespace WebTMDT_Client.Data
{
    public class Author
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public virtual IList<Book> Books { get; set; }
        public Author()
        {
            Books = new List<Book>();
        }
    }
}
