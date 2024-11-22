using CatsAndPies.Domain.DTO.Request;
using CatsAndPies.Domain.Entities;
using CatsAndPies.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.Abstractions.Services
{
    public interface IUserService
    {
        Task<Result<UserEntity?>> GetUserByLogin(string login);
        Task<Result<bool>> RegisterUser(RegisterRequestDto model);
    }
}
