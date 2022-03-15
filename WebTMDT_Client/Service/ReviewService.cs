using Newtonsoft.Json;
using System.Text;
using WebTMDT_Client.ResponseModel;
using WebTMDTLibrary.DTO;
using WebTMDTLibrary.Hepler;
using System.Net.Http.Headers;

namespace WebTMDT_Client.Service
{
    public class ReviewService : IReviewService
    {
        private readonly IConfiguration Configuration;
        public ReviewService(IConfiguration _configuration)
        {
            this.Configuration = _configuration;
        }
        public async  Task<PostReviewResponseModel> GetPostReviewResponse(PostReviewDTO dTO,string token)
        {

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Configuration["Setting:API_URL"]);
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
 
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",token );
                    string url = $"{Configuration["Setting:API_ENDPOINT:Review:Base"]}";

                    CreateReviewDTO postDTO = new CreateReviewDTO();
                    postDTO.BookId = dTO.BookId;
                    postDTO.Star = dTO.Star;
                    postDTO.Content = dTO.Content;
                    postDTO.Recomended = Boolean.Parse(dTO.Recomended);
                    postDTO.UserID = dTO.UserID;
                    postDTO.Date = DateTime.Now;

                    string json = JsonConvert.SerializeObject(postDTO);

                    var responseTask = client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
                    var result = await responseTask;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();

                        var data = readTask.Result;
                        var res = JsonConvert.DeserializeObject<PostReviewResponseModel>(data);
                        return res;
                    }
                    else //web api sent error response 
                    {
                        return new PostReviewResponseModel() { error = "Chưa nhập đủ thông tin!", success = false };
                    }
                }
            }
            catch (Exception ex)
            {
                return new PostReviewResponseModel() { error="Có gì lỗi rồi!",success=false};
            }
        }

        public async Task<DeleteReviewResponse> GetDeleteReviewResponse(DeleteReviewDTO dTO,string token)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Configuration["Setting:API_URL"]);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    string url = $"{ Configuration["Setting:API_URL"]}"+$"{Configuration["Setting:API_ENDPOINT:Review:Base"]}";


                    string json = JsonConvert.SerializeObject(dTO);
                    HttpRequestMessage request = new HttpRequestMessage
                    {
                        Content = new StringContent(json, Encoding.UTF8, "application/json"),
                        Method = HttpMethod.Delete,
                        RequestUri = new Uri(url)
                    };

                    var responseTask = client.SendAsync(request);
                    var result = await responseTask;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();

                        var data = readTask.Result;
                        //Console.WriteLine(data);
                        var res = JsonConvert.DeserializeObject<DeleteReviewResponse>(data);
                        return res;
                    }
                    else //web api sent error response 
                    {
                        return new DeleteReviewResponse() { error = "Có gì lỗi rồi!", success = false };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new DeleteReviewResponse() { error = "Có gì lỗi rồi!", success = false };
            }
        }
    }
}
