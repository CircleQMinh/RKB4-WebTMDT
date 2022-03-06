using WebTMDT_Client.ResponseModel;
using WebTMDTLibrary.DTO;

namespace WebTMDT_Client.Service
{
    public interface IAccountService
    {
        public Task<LoginResponseModel> Login(LoginUserDTO model);
        public Task<bool> Register(UserRegisterDTO dto);
        public Task<ConfirmEmailResponseModel> ConfirmEmail(ConfirmEmailDTO dto);
    }
}
