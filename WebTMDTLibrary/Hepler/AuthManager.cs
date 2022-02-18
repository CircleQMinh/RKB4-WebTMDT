

namespace DotNet6WebApi.Helper
{
    public class AuthManager : IAuthManager
    {
        //private readonly UserManager<AppUser> _userManager;
        //private readonly IConfiguration _configuration;
        //private AppUser _user;
        //public AuthManager(UserManager<AppUser> userManager,
        //    IConfiguration configuration)
        //{
        //    _userManager = userManager;
        //    _configuration = configuration;
        //}

        //public async Task<string> CreateToken()
        //{
        //    var signingCredentials = GetSigningCredentials();
        //    var claims = await GetClaims();
        //    var token = GenerateTokenOptions(signingCredentials, claims);

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}

        //private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        //{
        //    var jwtSettings = _configuration.GetSection("Jwt");
        //    var expiration = DateTime.Now.AddMinutes(Convert.ToDouble(
        //        jwtSettings.GetSection("lifetime").Value));

        //    var token = new JwtSecurityToken(
        //        issuer: jwtSettings.GetSection("Issuer").Value,
        //        claims: claims,
        //        expires: expiration,
        //        signingCredentials: signingCredentials
        //        );

        //    return token;
        //}

        //private async Task<List<Claim>> GetClaims()
        //{
        //    var claims = new List<Claim>
        //     {
        //         new Claim(ClaimTypes.Name, _user.UserName)
        //     };

        //    var roles = await _userManager.GetRolesAsync(_user);

        //    foreach (var role in roles)
        //    {
        //        claims.Add(new Claim(ClaimTypes.Role, role));
        //    }

        //    return claims;
        //}

        //private SigningCredentials GetSigningCredentials()
        //{
        //    var key = Convert.ToString(_configuration.GetSection("Jwt").GetSection("Key").Value);
        //    var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

        //    return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        //}

        //public async Task<bool> ValidateUser(LoginUserDTO userDTO)
        //{
        //    _user = await _userManager.FindByEmailAsync(userDTO.Email);
        //    //Console.WriteLine(userDTO.Email);
        //    //Console.WriteLine(userDTO.Password);
        //    var validPassword = await _userManager.CheckPasswordAsync(_user, userDTO.Password);

        //    return (_user != null && validPassword);
        //}
    }
}
