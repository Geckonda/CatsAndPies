using CatsAndPies.Domain.Abstractions.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.Models.Cat
{
    public class CuriousPersonality : PersonalityBehavior
    {
        private readonly List<string> _responses;
        private static readonly Random _random = new();


        public CuriousPersonality(IPersonalityDataProvider dataProvider)
        {
            _responses = dataProvider.GetResponses("CuriousPersonality");
            _greetings = dataProvider.GetGreetings("CuriousPersonality");
        }

        public override string GenerateResponse(string message)
        {
            string randomResponse = _responses[_random.Next(_responses.Count)];
            return $"{message} {randomResponse}";
        }

        public override string GenerateGreetings()
        {
            return _greetings[_random.Next(_greetings.Count)];
        }
        //Заглушка
        public override string GenerateRandomMessage()
        {
            return _responses[_random.Next(_responses.Count)];
        }
    }
}
