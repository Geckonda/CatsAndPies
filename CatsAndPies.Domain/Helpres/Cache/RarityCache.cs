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
        private List<RarityEntity> _cache; // Кеш редкостей

        public RarityCache(List<RarityEntity> rarities)
        {
            _cache = rarities;

        }
        public double GetChance(int rarityId)
        {
            return _cache.Where(x => x.Id == rarityId)
                .Select(x => x.Chance).First();
        }

        public List<RarityEntity> GetAll()
        {
            return _cache;
        }
        public RarityEntity GetRarityById(int id)
        {
            return _cache.FirstOrDefault(x => x.Id == id)!;
        }
    }

}
