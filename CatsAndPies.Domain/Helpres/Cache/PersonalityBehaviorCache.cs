using CatsAndPies.Domain.Models.Cat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.Helpres.Cache
{
    public static class PersonalityBehaviorCache
    {
        //Придумать, как перетягивать это из БД 
        private static readonly Dictionary<int, PersonalityBehavior> _cache = new()
        {
            { 1, new ArrogantPersonality() },
            { 2, new WisePersonality() },
            { 3, new CuriousPersonality() },
        };
        public static PersonalityBehavior GetPersonalityBehavior(int personalityType)
        {
            return _cache.ContainsKey(personalityType)
                ? _cache[personalityType]
                : throw new InvalidOperationException("Неизвестный тип характера кота");
        }
    }
}
