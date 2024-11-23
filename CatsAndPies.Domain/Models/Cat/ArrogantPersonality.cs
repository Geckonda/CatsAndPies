using CatsAndPies.Domain.Abstractions.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.Models.Cat
{
    public class ArrogantPersonality : PersonalityBehavior
    {
        private readonly List<string> _responses;
        private static readonly Random _random = new();

        public ArrogantPersonality(IPersonalityDataProvider dataProvider)
        {
            _responses = dataProvider.GetResponses("ArrogantPersonality");
            _greetings = dataProvider.GetGreetings("ArrogantPersonality");
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
        public override string GenerateRandomMessage()
        {
            return _responses[_random.Next(_responses.Count)];
        }
    }
}
