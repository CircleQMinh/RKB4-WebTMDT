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
    public class GenresDeserialize
    {
        public List<GenreDTO> result { get; set; }
    }
}
