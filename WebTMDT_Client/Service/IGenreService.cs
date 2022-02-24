using WebTMDTLibrary.DTO;

namespace WebTMDT_Client.Service
{
    public interface IGenreService
    {
        public GenreInfoDTO GetGenreInfo(int id);
        public List<GenreDTO> GetGenres();
    }
}
