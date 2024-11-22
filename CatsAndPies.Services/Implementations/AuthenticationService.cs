using CatsAndPies.Domain.Abstractions.Services;
using CatsAndPies.Domain.Abstractions.Services.Cat;
using CatsAndPies.Domain.DTO.Request;
using CatsAndPies.Domain.DTO.Response;
using CatsAndPies.Domain.Entities;
using CatsAndPies.Domain.Enums;
using CatsAndPies.Domain.Helpres;
using CatsAndPies.Domain.Models.Response;
using CatsAndPies.Domain.Responses;
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
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly ICatService _catService;
        private readonly ITokenService _tokenService;

        public AuthenticationService(IUserService userService,
            ICatService catService,
            ITokenService tokenService)
        {
            _userService = userService;
            _catService = catService;
            _tokenService = tokenService;
        }

        public async Task<Result<LoginResponseDto>> TryAuthenticate(LoginRequestDto model)
        {
            var userResult = await _userService.TryGetUserByLogin(model.Login);
            if (!userResult.IsSuccess)
                return Result<LoginResponseDto>.ErrorResult();

            var user = userResult.Data;
            if (user == null || user.Password != HashPasswordHelper.HashPassword(model.Password))
                return Result<LoginResponseDto>.ErrorResult();

            var token = _tokenService.GenerateJwtToken(user);

            var catResult = await _catService.TryGetCatWithoutOwnerByUserId(user.Id);

            LoginResponseDto data = new LoginResponseDto
            {
                Name = user.Name,
                Login = user.Login,
                Cat = catResult.Data,
                Token = token
            };
            return Result<LoginResponseDto>.SuccessResult(data);
        }
    }
}
