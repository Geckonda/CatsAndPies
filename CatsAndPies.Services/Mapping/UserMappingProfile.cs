using AutoMapper;
using CatsAndPies.Domain.DTO.Request;
using CatsAndPies.Domain.DTO.Response;
using CatsAndPies.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Services.Mapping
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserEntity, LoginRequestDto>().ReverseMap();
            CreateMap<UserEntity, LoginResponseDto>().ReverseMap();
            CreateMap<UserEntity, RegisterRequestDto>().ReverseMap();
            CreateMap<LoginRequestDto, RegisterRequestDto>().ReverseMap();
        }
    }
}
