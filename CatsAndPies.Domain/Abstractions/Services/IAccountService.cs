using CatsAndPies.Domain.Abstractions.Responses;
using CatsAndPies.Domain.DTO.Request;
using CatsAndPies.Domain.DTO.Response;
using CatsAndPies.Domain.Helpres;
using CatsAndPies.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.Abstractions.Services
{
    public interface IAccountService
    {
        Task<BaseResponse<LoginResponseDto>> Register(RegisterRequestDto model);

        Task<BaseResponse<LoginResponseDto>> Login(LoginRequestDto model);
    }
}
