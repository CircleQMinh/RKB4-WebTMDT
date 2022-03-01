using Newtonsoft.Json;
using WebTMDT_Client.ViewModel;
using WebTMDTLibrary.DTO;
using WebTMDTLibrary.Hepler;

namespace WebTMDT_Client.Service
{
    public class GenreService : IGenreService
    {
        public GenreInfoDTO GetGenre(int id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ProjectConst.API_URL);
                    var url = $"genre/{id}";
                    var responseTask = client.GetAsync(url);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        var data = readTask.Result;
                        //Console.WriteLine(data);
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

        public List<GenreDTO> GetGenres()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ProjectConst.API_URL);
                    var url = $"genre";
                    var responseTask = client.GetAsync(url);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        var data = readTask.Result;
                        //Console.WriteLine(data);
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
