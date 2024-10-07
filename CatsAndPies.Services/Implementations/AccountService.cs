using CatsAndPies.Domain.Abstractions.Repositories;
using CatsAndPies.Domain.Abstractions.Services;
using CatsAndPies.Domain.DTO.Request;
using CatsAndPies.Domain.DTO.Response;
using CatsAndPies.Domain.Entities;
using CatsAndPies.Domain.Enums;
using CatsAndPies.Domain.Helpres;
using CatsAndPies.Domain.Response;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly ILogger<AccountService> _logger;
        private readonly IBaseRepository<UserEntity> _userRepository;
        public AccountService(IBaseRepository<UserEntity> userRepository,
            ILogger<AccountService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<BaseResponse<LoginResponseDto>> Login(LoginRequestDto model)
        {
            try
            {
                var user = _userRepository
                    .GetAll().Result!
                    .Where(x => x.Login == model.Login)
                    .FirstOrDefault();

                if (user == null || user.Password != HashPasswordHelper.HashPassword(model.Password))
                {
                    return new()
                    {
                        StatusCode = StatusCode.Unauthorized,
                        Description = $"Провальная авторизация",
                        MessageForUser = "Логин или пароль указаны неверно",
                    };
                }
                var result = new LoginResponseDto()
                {
                    Name = user.Name,
                    Login = user.Login,
                    Token = Authenticate(user)
                };
                return new()
                {
                    StatusCode = StatusCode.Ok,
                    Data = result,
                    Description = $"Пользователь {user.Login} авторизировался",
                    MessageForUser = "Авторизация прошла успешно!"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Login]: {ex.Message}]");
                return new ()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError,
                    MessageForUser = "Не удалось авторизироваться. Что-то пошло не так..."
                };
            }
        }

        public async Task<BaseResponse<LoginResponseDto>> Register(RegisterRequestDto model)
        {
            try
            {
                var user = _userRepository
                    .GetAll().Result!
                    .Where(x => x.Login == model.Login)
                    .FirstOrDefault();
                if (user != null)
                    return new BaseResponse<LoginResponseDto>
                    {
                        StatusCode = StatusCode.Conflict,
                        MessageForUser = "Пользователь с таким логином же зарегистрирован",
                        Description = "Регистрация провалена. Причина: пользователь с таким логином же зарегистрирован"
                    };

                user = new UserEntity()
                {
                    RoleId = (int)RoleCode.User,
                    Name = model.Name,
                    Login = model.Login,
                    Password = HashPasswordHelper.HashPassword(model.Password),
                };
                await _userRepository.Add(user);
                var result = new LoginResponseDto()
                {
                    Name = user.Name,
                    Login = user.Login,
                    Token = Authenticate(user)
                };
                return new()
                {
                    StatusCode = StatusCode.Created,
                    Data = result,
                    MessageForUser = "Регистрация прошла успешно",
                    Description = $"Пользователь {user.Login} успешно зарегистрировался"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Register]: {ex.Message}]");
                return new()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError,
                    MessageForUser = "Не удалось зарегистрироваться. Что-то пошло не так..."
                };
            }
        }
        public TokenResult Authenticate(UserEntity user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("ThisIsMySuperSecretKeyForJwtToken1234567890");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.Login),
                        new Claim(ClaimTypes.Role, user.Role!.Name)
                    }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new()
            {
                Token = tokenHandler.WriteToken(token),
                ExpiresIn = tokenDescriptor.Expires
            };
        }

    }
}
