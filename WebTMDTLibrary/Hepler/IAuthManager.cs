
using WebTMDTLibrary.DTO;

namespace WebTMDTLibrary.Helper
{
    public interface IAuthManager
    {
        Task<bool> ValidateUser(LoginUserDTO userDTO);
        Task<string> CreateToken();
    }
}
