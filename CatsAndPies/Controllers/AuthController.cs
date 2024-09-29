using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using CatsAndPies.Domain.Abstractions.Services;
using CatsAndPies.Domain.DTO.Request;
using CatsAndPies.Domain.Enums;

namespace CatsAndPies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAccountService _accountService;
        public AuthController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var response = await _accountService.Login(model);
            if(response.StatusCode == Domain.Enums.StatusCode.Ok)
                return Ok(response);
            return Unauthorized();
        }
        [HttpPost("Registration")]
        public async Task<IActionResult> Registration([FromBody] RegisterRequestDto model)
        {
            var response = await _accountService.Register(model);
            if (response.StatusCode == Domain.Enums.StatusCode.Ok)
                return Ok(response);
            return Unauthorized(response);
        }
    }
}



//private readonly Dictionary<string, string> Users = new() {
//            { "Geckonda", "212121" },
//            { "Aqua", "121212" }
//        };

//[HttpPost]
//public IActionResult Login([FromBody] LoginRequest login)
//{
//    if (CheckUser(login.Username, login.Password))
//    {
//        var tokenHandler = new JwtSecurityTokenHandler();
//        var key = Encoding.ASCII.GetBytes("ThisIsMySuperSecretKeyForJwtToken1234567890");
//        var tokenDescriptor = new SecurityTokenDescriptor
//        {
//            Subject = new ClaimsIdentity(new[]
//            {
//                        new Claim(ClaimTypes.Name, login.Username),
//                        new Claim(ClaimTypes.Role, "Admin")
//                    }),
//            Expires = DateTime.UtcNow.AddHours(1),
//            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
//        };
//        var token = tokenHandler.CreateToken(tokenDescriptor);
//        return Ok(new
//        {
//            token = tokenHandler.WriteToken(token),
//            expiresIn = tokenDescriptor.Expires
//        });
//    }
//    return Unauthorized();
//}
//private bool CheckUser(string username, string userpassword)
//{
//    if (Users.TryGetValue(username, out var password))
//    {
//        return password == userpassword;
//    }
//    return false;
//}
//    }

//    public class LoginRequest
//{
//    public string Username { get; set; }
//    public string Password { get; set; }
//}

