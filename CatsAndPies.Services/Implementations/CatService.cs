using AutoMapper;
using CatsAndPies.Domain.Abstractions.Repositories.Combined;
using CatsAndPies.Domain.Abstractions.Services.Cat;
using CatsAndPies.Domain.DTO.Response;
using CatsAndPies.Domain.DTO.Response.Cat;
using CatsAndPies.Domain.Entities;
using CatsAndPies.Domain.Enums;
using CatsAndPies.Domain.Factories;
using CatsAndPies.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Services.Implementations
{
    public class CatService : ICatCreationService, ICatMessageService
    {
        public readonly ICatRepository _catRepository;
        public readonly CatFactory _catFactory;
        private readonly IMapper _mapper;
        public CatService(ICatRepository catRepository,
            CatFactory catFactory,
            IMapper mapper)
        {
            _catRepository = catRepository;
            _catFactory = catFactory;
            _mapper = mapper;
        }
        public async Task<BaseResponse<CatResponseDTO>> CreateCat(string name, int userId)
        {
            try
            {
                var (colorId, personalityId) = await _catRepository.GetRandomColorAndPersonality();
                CatEntity entity = new()
                {
                    Name = name,
                    UserId = userId,
                    AdoptedTime = DateTime.Now,
                    ColorId = colorId,
                    PersonalityId = personalityId,
                };

                await _catRepository.Add(entity);
                entity = await _catRepository.GetOneById(entity.Id);
                var cat = _catFactory.CreateCatWithCertainBehavior(personalityId);
                var model = _mapper.Map<CatResponseDTO>(entity);
                model.Phrase = cat.SayHelloToNewOwner();
                return new BaseResponse<CatResponseDTO>
                {
                    StatusCode = StatusCode.Created,
                    Data = model,
                    MessageForUser = $"Твой новый кот {name}",
                    Description = "Кот создан"
                };
            }
            catch (Exception ex)
            {
                return new()
                {
                    StatusCode = StatusCode.InternalServerError,
                    MessageForUser = "Не найти кота для тебя, что-то пошло не так...",
                    Description = ex.Message,
                };
            }
        }
    }
}
