using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.Models.Cat
{
    public abstract class PersonalityBehavior
    {
        protected List<string> _greetings;
        public abstract string GenerateGreetings();
        public abstract string GenerateResponse(string message);
        public abstract string GenerateRandomMessage();
    }
}
