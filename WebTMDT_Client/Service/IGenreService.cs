using WebTMDTLibrary.DTO;

namespace WebTMDT_Client.Service
{
    public interface IGenreService
    {
        public GenreInfoDTO GetGenre(int id);
        public List<GenreDTO> GetGenres();
    }
}
