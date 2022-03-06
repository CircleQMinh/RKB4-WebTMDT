using Newtonsoft.Json;
using WebTMDT_Client.ViewModel;
using WebTMDTLibrary.DTO;
using WebTMDTLibrary.Hepler;

namespace WebTMDT_Client.Service
{
    public class GenreService : IGenreService
    {
        private readonly IConfiguration Configuration;
        public GenreService(IConfiguration _configuration)
        {
            this.Configuration = _configuration;
        }
        public async Task<GenreInfoDTO> GetGenre(int id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Configuration["Setting:API_URL"]);
                    var url = $"genre/{id}";
                    var responseTask = client.GetAsync(url);
                    var result = await responseTask;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        var data = readTask.Result;

                        var genre = JsonConvert.DeserializeObject<GenreDeserialize>(data);
                        return genre.result;
                    }
                    else //web api sent error response 
                    {
                        Console.WriteLine(result.StatusCode);
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<GenreDTO>> GetGenres()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Configuration["Setting:API_URL"]);
                    var url = $"genre";
                    var responseTask = client.GetAsync(url);
                    var result = await responseTask;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        var data = readTask.Result;
                        var genre = JsonConvert.DeserializeObject<GenresDeserialize>(data);
                        return genre.result;
                    }
                    else //web api sent error response 
                    {
                        Console.WriteLine(result.StatusCode);
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
