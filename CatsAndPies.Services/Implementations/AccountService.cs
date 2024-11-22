using AutoMapper;
using CatsAndPies.Domain.Abstractions.Repositories;
using CatsAndPies.Domain.Abstractions.Repositories.Combined;
using CatsAndPies.Domain.Abstractions.Services;
using CatsAndPies.Domain.DTO.Request;
using CatsAndPies.Domain.DTO.Response;
using CatsAndPies.Domain.DTO.Response.Cat;
using CatsAndPies.Domain.Entities;
using CatsAndPies.Domain.Enums;
using CatsAndPies.Domain.Factories;
using CatsAndPies.Domain.Helpres;
using CatsAndPies.Domain.Models.Response;
using CatsAndPies.Domain.Responses;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IAuthenticationService _authService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public AccountService(IAuthenticationService authService,
            IUserService userService,
            IMapper mapper)
        {
            _authService = authService;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<Result<LoginResponseDto>> TryLogin(LoginRequestDto model)
        {
            return await _authService.TryAuthenticate(model);
        }

        public async Task<Result<LoginResponseDto>> TryRegister(RegisterRequestDto model)
        {
            var result = await _userService.TryRegisterUser(model);
            if(!result.IsSuccess)
                return  Result<LoginResponseDto>.ErrorResult();
            return await _authService.TryAuthenticate(_mapper.Map<LoginRequestDto>(model));
        }
    }
}