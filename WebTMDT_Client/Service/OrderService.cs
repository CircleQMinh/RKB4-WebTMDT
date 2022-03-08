using AutoMapper;
using Newtonsoft.Json;
using System.Text;
using WebTMDT_Client.ResponseModel;
using WebTMDTLibrary.DTO;
using WebTMDTLibrary.Helper;
using WebTMDTLibrary.Hepler;
using System.Net.Http.Headers;

namespace WebTMDT_Client.Service
{
    public class OrderService : IOrderService

    {
        private readonly IMapper mapper;
        private readonly IConfiguration Configuration;
        public OrderService(IMapper _mapper, IConfiguration _configuration)
        {
            mapper = _mapper;
            this.Configuration = _configuration;
        }

        public async Task<PostOrderResponseModel> GetPostOrderResponse(PostOrderDTO dto, Cart cart,string token,string userId)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Configuration["Setting:API_URL"]);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    string url = $"{Configuration["Setting:API_ENDPOINT:Order:PostOrder"]}";
                    var postDTO = mapper.Map<CreateOrderDTO>(dto);
                    var orderDetails = new List<CreateOrderDetailDTO>();
                    foreach (var item in cart.Items)
                    {
                        var od = mapper.Map<CreateOrderDetailDTO>(item);
                        orderDetails.Add(od);
                    }
                    postDTO.OrderDetails = orderDetails;
                    postDTO.TotalPrice = cart.TotalPrice;
                    postDTO.TotalItem = cart.TotalItem;
                    postDTO.UserID = userId;
                    postDTO.OrderDate = DateTime.Now;
                    postDTO.Status = 0;
                    postDTO.Address = Utility.ConstructAddressString(dto.AddressNo,dto.Street,dto.District,dto.Ward,dto.City);

                    string json = JsonConvert.SerializeObject(postDTO);
                    string data = "";
                    var responseTask = client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
                    var result = await responseTask;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        data = readTask.Result;
                        var res = JsonConvert.DeserializeObject<PostOrderResponseModel>(data);
                        return res;
                    }
                    else //web api sent error response 
                    {
                        var readTask = result.Content.ReadAsStringAsync();

                        data = readTask.Result;
                        Console.WriteLine(result.StatusCode);
                        Console.WriteLine(data);
                        return new PostOrderResponseModel() { success = false };
                    }
                }
            }
            catch (Exception ex)
            {
                return new PostOrderResponseModel() { success = false };
            }
        }

        public async Task<string> GetVNPayUrl(double totalPrice,string token)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Configuration["Setting:API_URL"]);
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    string url = $"{Configuration["Setting:API_ENDPOINT:Order:GetVNPayUrl"]}?totalPrice={totalPrice}";

                    string data = "";
                    var responseTask = client.GetAsync(url);
                    var result = await responseTask;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        data = readTask.Result;
                        var res = JsonConvert.DeserializeObject<GetVNPayUrlResponseModel>(data);
                        return res.paymentUrl;
                    }
                    else //web api sent error response 
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        data = readTask.Result;
                        Console.WriteLine(result.StatusCode);
                        Console.WriteLine(data);
                        return String.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                return String.Empty;
            }
        }
    }
}
