using AutoMapper;
using CatsAndPies.Domain.Abstractions.Repositories.Combined;
using CatsAndPies.Domain.Abstractions.Services;
using CatsAndPies.Domain.DTO.Response.Cat;
using CatsAndPies.Domain.DTO.Response.Pie;
using CatsAndPies.Domain.Entities.PiesTables;
using CatsAndPies.Domain.Helpres.Cache;
using CatsAndPies.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Services.Implementations
{
    public class PieService : IPieService
    {
        private static readonly Random random = new Random();
        private readonly IPieRepository _pieRepository;
        private readonly RarityCache _rarityCache;
        private readonly PieEffectsCache _effectCache;
        private readonly IMapper _mapper;

        public PieService(IPieRepository pieRepository,
            RarityCache rarityChancesCache,
            PieEffectsCache pieEffectsCache,
            IMapper mapper)
        {
            _pieRepository = pieRepository;
            _rarityCache = rarityChancesCache;
            _effectCache = pieEffectsCache;
            _mapper = mapper;
        }

        public async Task<Result<PieResponseDTO>> TryCreatePie(int userId, string pieName)
        {
            var rarity = GenerateRarity();
            var effect = _effectCache.GetEffectByRarityId(rarity.Id);
            PieEntity entity = new()
            {
                Name = pieName,
                Price = 0,
                Created = DateTime.Now,
                ImgLink = "catsandpies.ru/images/defaults/pieDefault.jpg",
                EffectId = effect.Id,
                OwnerId = userId,
            };
            await _pieRepository.Add(entity);
            var model = _mapper.Map<PieResponseDTO>(entity);
            model.Rarity = _mapper.Map<PieRarityResponseDTO>(rarity);
            model.Effect = _mapper.Map<PieEffectResponseDTO>(effect);
            return Result<PieResponseDTO>.SuccessResult(model);
        }

        public async Task<Result<List<PieEntity>>> TryGetAllPies()
        {
            var pies = await _pieRepository.GetAll();
            if (pies.Count == 0)
                return Result<List<PieEntity>>.ErrorResult();
            return Result<List<PieEntity>>.SuccessResult(pies);
        }

        private RarityEntity GenerateRarity()
        {
            var rarities = _rarityCache.GetAll();
            // Генерируем случайное число от 0 до 100
            double randomValue = random.NextDouble() * 100;

            // Проходим по шансам и определяем редкость
            double cumulativeChance = 0;
            foreach (var rarity in rarities)
            {
                cumulativeChance += rarity.Chance;
                if (randomValue <= cumulativeChance)
                    return rarity;
            }
            return rarities.FirstOrDefault()!;
        }
    }
}
