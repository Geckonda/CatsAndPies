using CatsAndPies.Domain.Entities;
using CatsAndPies.Domain.Helpres.Cache;
using CatsAndPies.Domain.Models.Cat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.Factories
{
    public class CatFactory
    {
        public Cat CreateCatWithCertainBehavior(int behaviourType)
        {
            var behaviour = PersonalityBehaviorCache.GetPersonalityBehavior(behaviourType);
            return new Cat(behaviour);
        }
    }
}
