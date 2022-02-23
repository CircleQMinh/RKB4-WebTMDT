using Newtonsoft.Json;
using System.Text;
using WebTMDT_Client.ResponseModel;
using WebTMDTLibrary.DTO;
using WebTMDTLibrary.Hepler;

namespace WebTMDT_Client.Service
{
    public class ReviewService : IReviewService
    {
        public PostReviewResponse GetPostReviewResponse(PostReviewDTO dTO,string token)
        {

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ProjectConst.API_URL);
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
 
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",token );
                    string url = "review";

                    CreateReviewDTO postDTO = new CreateReviewDTO();
                    postDTO.BookId = dTO.BookId;
                    postDTO.Star = dTO.Star;
                    postDTO.Content = dTO.Content;
                    postDTO.Recomended = Boolean.Parse(dTO.Recomended);
                    postDTO.UserID = dTO.UserID;
                    postDTO.Date = DateTime.Now;

                    string json = JsonConvert.SerializeObject(postDTO);

                    var responseTask = client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        var data = readTask.Result;
                        //Console.WriteLine(data);
                        var res = JsonConvert.DeserializeObject<PostReviewResponse>(data);
                        return res;
                    }
                    else //web api sent error response 
                    {
                        return new PostReviewResponse() { error = "Có gì lỗi rồi!", success = false };
                    }
                }
            }
            catch (Exception ex)
            {
                return new PostReviewResponse() { error="Có gì lỗi rồi!",success=false};
            }
        }

        public DeleteReviewResponse GetDeleteReviewResponse(DeleteReviewDTO dTO,string token)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ProjectConst.API_URL);
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    string url = ProjectConst.API_URL+"review";


                    string json = JsonConvert.SerializeObject(dTO);
                    HttpRequestMessage request = new HttpRequestMessage
                    {
                        Content = new StringContent(json, Encoding.UTF8, "application/json"),
                        Method = HttpMethod.Delete,
                        RequestUri = new Uri(url)
                    };
                    Console.WriteLine();
                    Console.WriteLine(request.RequestUri.ToString());
                    var responseTask = client.SendAsync(request);
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
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
