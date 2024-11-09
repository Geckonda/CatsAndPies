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
using System.Xml.Linq;

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
                var entity = await _catRepository.GetOneByUserId(userId);
                if(entity != null)
                {
                    return new BaseResponse<CatResponseDTO>
                    {
                        StatusCode = StatusCode.Conflict,
                        Data = null,
                        MessageForUser = $"У вас уже есть кот {entity.Name}.",
                        Description = "Кот уже был создан ранее."
                    };
                }
                var (colorId, personalityId) = await _catRepository.GetRandomColorAndPersonality();
                entity = new()
                {
                    Name = name,
                    UserId = userId,
                    AdoptedTime = DateTime.Now,
                    ColorId = colorId,
                    PersonalityId = personalityId,
                };

                await _catRepository.Add(entity);
                entity = await _catRepository.GetOneByUserId(userId);
                var cat = _catFactory.CreateCatWithCertainBehavior(personalityId);
                var model = _mapper.Map<CatResponseDTO>(entity);
                model.Phrase = cat.SayHelloToNewOwner();
                return new BaseResponse<CatResponseDTO>
                {
                    StatusCode = StatusCode.Created,
                    Data = model,
                    MessageForUser = $"Ваш новый кот {name}",
                    Description = "Кот создан"
                };
            }
            catch (Exception ex)
            {
                return new()
                {
                    StatusCode = StatusCode.InternalServerError,
                    MessageForUser = "Не найти кота для вас, что-то пошло не так...",
                    Description = ex.Message,
                };
            }
        }

        public async Task<BaseResponse<string>> SaySomething(int userId)
        {
            try
            {
                var personalityId = await _catRepository.GetCatBehaviorByUserId(userId);
                if(personalityId == 0)
                {
                    return new BaseResponse<string>
                    {
                        StatusCode = StatusCode.NotFound,
                        Data = null,
                        MessageForUser = $"У вас нет кота.",
                        Description = "Кот не создан."
                    };
                }
                var cat = _catFactory.CreateCatWithCertainBehavior(personalityId);
                return new BaseResponse<string>
                {
                    StatusCode = StatusCode.Ok,
                    Data = cat.SaySomething(),
                    MessageForUser = $"Кот что-то говорит.",
                    Description = "Кот вернул сообщение"
                };
            }
            catch (Exception ex)
            {
                return new()
                {
                    StatusCode = StatusCode.InternalServerError,
                    MessageForUser = "Кот не стал отвечать, что-то пошло не так...",
                    Description = ex.Message,
                };
            }
        }
    }
}
