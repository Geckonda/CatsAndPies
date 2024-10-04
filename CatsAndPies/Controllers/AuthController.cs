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
