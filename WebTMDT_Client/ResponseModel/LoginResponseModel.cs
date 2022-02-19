using WebTMDTLibrary.DTO;

namespace WebTMDT_Client.ResponseModel
{
    public class LoginResponseModel
    {
        public bool success { get; set; }
        public string token { get; set; }
        public string msg { get; set; }
        public SimpleUserDTO user { get; set; }
    }
}
