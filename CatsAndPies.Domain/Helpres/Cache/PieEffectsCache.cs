using CatsAndPies.Domain.Abstractions.Repositories.Combined;
using CatsAndPies.Domain.Entities.PiesTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.Helpres.Cache
{
    public class PieEffectsCache
    {
        private Random _random = new Random();
        private List<PieEffectEntity> _cache;
        public PieEffectsCache(List<PieEffectEntity> effects)
        {
            _cache = effects;
        }
        public List<PieEffectEntity> GetAll()
        {
            return _cache;
        }
        public PieEffectEntity GetEffectByRarityId(int rarityId)
        {
            var filtered = _cache.Where(x => x.RarityId == rarityId).ToList();
            return filtered[_random.Next(filtered.Count)];
        }
    }
}
