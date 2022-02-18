namespace WebTMDTLibrary.DTO
{
    public class AuthorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class DetailAuthorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual IList<SimpleBookInfoDTO> Books { get; set; }
    }
}
