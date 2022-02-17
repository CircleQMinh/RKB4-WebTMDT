namespace WebTMDT.DTO
{
    public class LoginUserDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class UserRegisterDTO:LoginUserDTO
    {

        public string imgUrl { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public IList<string> Roles { get; set; }
    }
    public class UpdateUserDTO
    {
        public string imgUrl { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
    }
    public class SimpleUserDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string imgUrl { get; set; }
        public int Coins { get; set; }
        public IList<string> Roles { get; set; }
    }

    public class SimpleUserForAdminDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string imgUrl { get; set; }
        public int Coins { get; set; }
        public virtual bool EmailConfirmed { get; set; }
        public IList<string> Roles { get; set; }
    }


    public class ConfirmEmailDTO
    {
        public string email { get; set; }
        public string token { get; set; }
    }
    public class ResetPasswordDTO
    {
 
        public string Password { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
