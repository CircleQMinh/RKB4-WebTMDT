namespace WebTMDTLibrary.DTO
{
    public class GenreDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        //public virtual IList<Game> Games { get; set; }
    }
    public class SmallerGenreDTO
    {
        public string? Name { get; set; }
        //public virtual IList<Game> Games { get; set; }
    }
    public class GenreInfoDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Description { get; set; }
    }
    public class GenresDeserialize
    {
        public List<GenreDTO> result { get; set; }
    }
    public class GenreDeserialize
    {
        public GenreInfoDTO result { get; set;}
    }
}
