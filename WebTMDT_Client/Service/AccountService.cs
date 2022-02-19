using Newtonsoft.Json;
using System.Text;
using WebTMDT_Client.ResponseModel;
using WebTMDTLibrary.DTO;
using WebTMDTLibrary.Hepler;

namespace WebTMDT_Client.Service
{
    public class AccountService : IAccountService
    {
        public LoginResponseModel Login(LoginUserDTO model)
        {
            var login_response = new LoginResponseModel();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ProjectConst.API_URL);
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    string url = $"account/login";
                    string json = JsonConvert.SerializeObject(model);

                    var responseTask = client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        var data = readTask.Result;
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

        public bool Register(UserRegisterDTO dto)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ProjectConst.API_URL);
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    string url = $"account/register";
                    string json = JsonConvert.SerializeObject(dto);
                    var responseTask = client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
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

        public ConfirmEmailResponseModel ConfirmEmail(ConfirmEmailDTO dto)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ProjectConst.API_URL);
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    string url = $"account/confirmEmail";
                    string json = JsonConvert.SerializeObject(dto);
                    var responseTask = client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
                    responseTask.Wait();
                    var result = responseTask.Result;
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
