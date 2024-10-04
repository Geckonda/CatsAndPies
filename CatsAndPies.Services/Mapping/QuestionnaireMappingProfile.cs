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
    public class QuestionnaireMappingProfile : Profile
    {
        public QuestionnaireMappingProfile()
        {
            CreateMap<QuestionnaireEntity, QuestionnaireRequestDto>().ReverseMap();
            CreateMap<QuestionnaireEntity, QuestionnaireResponseDto>().ReverseMap();
        }
    }
}
