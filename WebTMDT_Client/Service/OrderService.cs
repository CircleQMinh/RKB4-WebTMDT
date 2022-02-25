using AutoMapper;
using Newtonsoft.Json;
using System.Text;
using WebTMDT_Client.ResponseModel;
using WebTMDTLibrary.DTO;
using WebTMDTLibrary.Helper;
using WebTMDTLibrary.Hepler;

namespace WebTMDT_Client.Service
{
    public class OrderService : IOrderService
    {
        private readonly IMapper mapper;
        public OrderService(IMapper _mapper)
        {
            mapper = _mapper;
        }

        public PostOrderResponseModel GetPostOrderResponse(PostOrderDTO dto, Cart cart,string token,string userId)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ProjectConst.API_URL);
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    string url = "order";
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
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        data = readTask.Result;
                        var res = JsonConvert.DeserializeObject<PostOrderResponseModel>(data);
                        return res;
                    }
                    else //web api sent error response 
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
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

        public string GetVNPayUrl(double totalPrice,string token)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ProjectConst.API_URL);
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    string url = $"Order/getVNPayUrl?totalPrice={totalPrice}";

                    string data = "";
                    var responseTask = client.GetAsync(url);
                    var result = responseTask.Result;
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
