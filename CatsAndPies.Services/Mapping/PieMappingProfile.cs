using AutoMapper;
using CatsAndPies.Domain.DTO.Response.Cat;
using CatsAndPies.Domain.Entities.Cats;
using CatsAndPies.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatsAndPies.Domain.Entities.PiesTables;
using CatsAndPies.Domain.DTO.Response.Pie;

namespace CatsAndPies.Services.Mapping
{
    public class PieMappingProfile : Profile
    {
        public PieMappingProfile()
        {
            CreateMap<PieEntity, PieResponseDTO>().ReverseMap();
            CreateMap<RarityEntity, PieRarityResponseDTO>().ReverseMap();
            CreateMap<PieEffectEntity, PieEffectResponseDTO>().ReverseMap();
        }
    }
}
