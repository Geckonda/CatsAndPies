using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using CatsAndPies.Domain.Abstractions.Services;
using CatsAndPies.Domain.DTO.Request;
using CatsAndPies.Domain.Enums;
using Swashbuckle.AspNetCore.Annotations;
using CatsAndPies.Domain.DTO.Response;
using CatsAndPies.Domain.Models.Response;

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
        [SwaggerOperation(Summary = "Авторизация", Description = "Возвращает имя, логин и токен авторизированного пользователя.")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            BaseResponse<LoginResponseDto> response;
            var result = await _accountService.Login(model);
            if(result.IsSuccess)
            {
                response = new()
                {
                    StatusCode = Domain.Enums.StatusCode.Ok,
                    Data = result.Data,
                    MessageForUser = "Авторизация прошла успешно."
                };
                return Ok(result.Data);
            }
            response = new()
            {
                StatusCode = Domain.Enums.StatusCode.Unauthorized,
                Data = result.Data,
                MessageForUser = "Введены неверные данные."
            };
            return Unauthorized(response);
        }
        [HttpPost("Registration")]
        [SwaggerOperation(Summary = "Регистрация", Description = "Возвращает имя, логин и токен авторизированного пользователя.")]
        public async Task<IActionResult> Registration([FromBody] RegisterRequestDto model)
        {
            BaseResponse<LoginResponseDto> response;
            var result = await _accountService.Register(model);
            if (result.IsSuccess)
            {
                response = new()
                {
                    StatusCode = Domain.Enums.StatusCode.Ok,
                    Data = result.Data,
                    MessageForUser = "Регистрация прошла успешно."
                };
                return CreatedAtAction("Registration", response);
            }
            response = new()
            {
                StatusCode = Domain.Enums.StatusCode.Conflict,
                Data = result.Data,
                MessageForUser = "Пользователь с таким логином же зарегистрирован",
            };
            return Conflict(response);
        }
    }
}
