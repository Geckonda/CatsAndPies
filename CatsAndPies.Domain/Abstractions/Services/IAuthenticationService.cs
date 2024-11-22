using CatsAndPies.Domain.DTO.Request;
using CatsAndPies.Domain.DTO.Response;
using CatsAndPies.Domain.Entities;
using CatsAndPies.Domain.Helpres;
using CatsAndPies.Domain.Models.Response;
using CatsAndPies.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.Abstractions.Services
{
    public interface IAuthenticationService
    {
        Task<Result<LoginResponseDto>> TryAuthenticate(LoginRequestDto model);
    }
}
