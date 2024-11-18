using AutoMapper;
using CatsAndPies.Domain.DTO.Request;
using CatsAndPies.Domain.DTO.Response;
using CatsAndPies.Domain.DTO.Response.Cat;
using CatsAndPies.Domain.Entities;
using CatsAndPies.Domain.Entities.Cats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Services.Mapping
{
    public class CatMappingProfile : Profile
    {
        public CatMappingProfile()
        {
            CreateMap<CatEntity, CatResponseDTO>().ReverseMap();
            CreateMap<CatEntity, CatResponseWithoutOwnerDTO>().ReverseMap();
            CreateMap<CatsColorEntity, CatColorResponseDTO>().ReverseMap();
            CreateMap<CatsPersonalityEntity, CatPersonalityResponseDTO>().ReverseMap();
            CreateMap<UserEntity, CatOwnerResponseDTO>().ReverseMap();
        }
    }
}
