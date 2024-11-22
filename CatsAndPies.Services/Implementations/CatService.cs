using AutoMapper;
using CatsAndPies.Domain.Abstractions.Repositories.Combined;
using CatsAndPies.Domain.Abstractions.Services;
using CatsAndPies.Domain.Abstractions.Services.Cat;
using CatsAndPies.Domain.DTO.Response;
using CatsAndPies.Domain.DTO.Response.Cat;
using CatsAndPies.Domain.Entities;
using CatsAndPies.Domain.Enums;
using CatsAndPies.Domain.Factories;
using CatsAndPies.Domain.Models.Response;
using CatsAndPies.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CatsAndPies.Services.Implementations
{
    public class CatService : ICatService
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

        public async Task<Result<CatResponseDTO?>> CreateCat(string name, int userId)
        {
            var entity = await _catRepository.GetOneByUserId(userId);
            if (entity != null)
                return Result<CatResponseDTO?>.ErrorResult();

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
            return Result<CatResponseDTO?>.SuccessResult(model);
        }

        public async Task<Result<CatResponseWithoutOwnerDTO?>> GetCatWithoutOwnerByUserId(int userId)
        {
            var entity = await _catRepository.GetOneByUserId(userId);
            if(entity == null)
                return Result<CatResponseWithoutOwnerDTO?>.ErrorResult();

            var cat = _catFactory.CreateCatWithCertainBehavior(entity.PersonalityId);
            CatResponseWithoutOwnerDTO catDTO = _mapper.Map<CatResponseWithoutOwnerDTO>(entity);
            catDTO.Phrase = "Мяу";
            return Result<CatResponseWithoutOwnerDTO?>.SuccessResult(catDTO);
        }

        public async Task<Result<string>> SaySomething(int userId)
        {
            var personalityId = await _catRepository.GetCatBehaviorByUserId(userId);
            if(personalityId == 0)
                return Result<string>.ErrorResult();

            var cat = _catFactory.CreateCatWithCertainBehavior(personalityId);
            return Result<string>.SuccessResult(cat.SaySomething());
        }
    }
}
