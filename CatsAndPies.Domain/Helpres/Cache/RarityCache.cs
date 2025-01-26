using CatsAndPies.Domain.Abstractions.Repositories.Combined;
using CatsAndPies.Domain.Entities.PiesTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.Helpres.Cache
{
    public class RarityCache
    {
        //private readonly IRarityRepository _rarityRepository;
        private List<RarityEntity> _cache; // Кеш редкостей

        public RarityCache(List<RarityEntity> rarities)
        {
            //_rarityRepository = rarityRepository ?? throw new ArgumentNullException(nameof(rarityRepository));
            _cache = rarities;

        }

        // Метод для загрузки данных в кеш
        //private async Task LoadCacheAsync()
        //{
        //    var rarities = await _rarityRepository.GetAll(); // Получаем данные из репозитория
        //    _cache.Clear(); // Очищаем старый кеш

        //    _cache = rarities;
        //}

        // Получение шанса по Id редкости
        public double GetChance(int rarityId)
        {
            return _cache.Where(x => x.Id == rarityId)
                .Select(x => x.Chance).First();
        }

        public List<RarityEntity> GetAll()
        {
            return _cache;
        }

        // Метод для обновления кеша вручную
        //public void RefreshCache()
        //{
        //    LoadCache();
        //}
    }

}
