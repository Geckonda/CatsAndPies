using CatsAndPies.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.Models.Cat
{
    public class Cat
    {
        public PersonalityBehavior PersonalityBehavior { get; private set; }

        public Cat(PersonalityBehavior behavior)
        {
            PersonalityBehavior = behavior;
        }
        public string Speak(string message)
        {
            return PersonalityBehavior.GenerateResponse(message);
        }
        public string SayHelloToNewOwner()
        {
            return PersonalityBehavior.GenerateGreetings();
        }
    }
}
