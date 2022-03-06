using WebTMDTLibrary.DTO;

namespace WebTMDT_Client.Service
{
    public interface IGenreService
    {
        public Task<GenreInfoDTO> GetGenre(int id);
        public Task<List<GenreDTO>> GetGenres();
    }
}
