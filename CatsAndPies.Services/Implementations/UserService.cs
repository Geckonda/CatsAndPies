using CatsAndPies.Domain.Abstractions.Repositories.Combined;
using CatsAndPies.Domain.Abstractions.Services;
using CatsAndPies.Domain.DTO.Request;
using CatsAndPies.Domain.Entities;
using CatsAndPies.Domain.Enums;
using CatsAndPies.Domain.Helpres;
using CatsAndPies.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Result<UserEntity?>> TryGetUserByLogin(string login)
        {
            var user = await _userRepository.GetOneByLogin(login);
            if (user == null)
                return Result<UserEntity?>.ErrorResult();
            return Result<UserEntity?>.SuccessResult(user);
        }

        public async Task<Result<bool>> TryRegisterUser(RegisterRequestDto model)
        {
            var user = await _userRepository.GetOneByLogin(model.Login);
            if (user != null)
                return Result<bool>.ErrorResult();

            var hashedPassword = HashPasswordHelper.HashPassword(model.Password);
            user = new UserEntity
            {
                Name = model.Name,
                Login = model.Login,
                Password = hashedPassword,
                RoleId = (int)RoleCode.User
            };
            await _userRepository.Add(user);

            return Result<bool>.SuccessResult(true);
        }
    }
}
