
using WebTMDTLibrary.DTO;

namespace WebTMDT_API.Authorize
{
    public interface IAuthManager
    {
        Task<bool> ValidateUser(LoginUserDTO userDTO);
        Task<string> CreateToken();
    }
}
