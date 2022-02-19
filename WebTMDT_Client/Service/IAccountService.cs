using WebTMDT_Client.ResponseModel;
using WebTMDTLibrary.DTO;

namespace WebTMDT_Client.Service
{
    public interface IAccountService
    {
        public LoginResponseModel Login(LoginUserDTO model);
        public bool Register(UserRegisterDTO dto);
        public ConfirmEmailResponseModel ConfirmEmail(ConfirmEmailDTO dto);
    }
}
