using Newtonsoft.Json;
using System.Text;
using WebTMDT_Client.ResponseModel;
using WebTMDTLibrary.DTO;
using WebTMDTLibrary.Hepler;
using System.Net.Http.Headers;

namespace WebTMDT_Client.Service
{
    public class AccountService : IAccountService
    {
        private readonly IConfiguration Configuration;
        public AccountService(IConfiguration _configuration)
        {
            this.Configuration = _configuration;
        }
        public async Task<LoginResponseModel> Login(LoginUserDTO model)
        {
            var login_response = new LoginResponseModel();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Configuration["Setting:API_URL"]);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string url = $"{Configuration["Setting:API_ENDPOINT:Account:Login"]}";
                    string json = JsonConvert.SerializeObject(model);

                    var responseTask = client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
                    var result = await responseTask;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
 
                        var data = readTask.Result;
                        Console.Write(data);
                        login_response = JsonConvert.DeserializeObject<LoginResponseModel>(data);
                    }
                    else //web api sent error response 
                    {
                        Console.WriteLine(result.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                login_response.success = false;
                return login_response;
            }
            return login_response;
        }

        public async Task<bool> Register(UserRegisterDTO dto)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Configuration["Setting:API_URL"]);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string url = $"{Configuration["Setting:API_ENDPOINT:Account:Register"]}";
                    string json = JsonConvert.SerializeObject(dto);
                    var responseTask = client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
                    var result = await responseTask;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
 
                        var data = readTask.Result;
                        RegisterResponeModel register_res = JsonConvert.DeserializeObject<RegisterResponeModel>(data);
                        if (register_res.success)
                        {
                            return true;
                        }
                    }
                    else //web api sent error response 
                    {
                        Console.WriteLine(result.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return false;
        }

        public async Task<ConfirmEmailResponseModel> ConfirmEmail(ConfirmEmailDTO dto)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Configuration["Setting:API_URL"]);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string url = $"{Configuration["Setting:API_ENDPOINT:Account:ConfirmEmail"]}";
                    string json = JsonConvert.SerializeObject(dto);
                    var responseTask = client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
                    var result = await responseTask;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        var data = readTask.Result;
                        ConfirmEmailResponseModel res = JsonConvert.DeserializeObject<ConfirmEmailResponseModel>(data);
                        return res;
                      
                    }
                    else //web api sent error response 
                    {
                        Console.WriteLine(result.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ConfirmEmailResponseModel { success=false,message="Có lỗi xảy ra!"};
            }
            return new ConfirmEmailResponseModel { success = false, message = "Có lỗi xảy ra!" };
        }
    }
}
