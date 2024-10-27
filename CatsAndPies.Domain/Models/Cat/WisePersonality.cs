using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.Models.Cat
{
    public class WisePersonality : PersonalityBehavior
    {
        private readonly List<string> _responses;
        private static readonly Random _random = new();

        public WisePersonality()
        {
            _responses = new List<string>
            {
                "Терпение — ключ к любому ларцу, мой друг.",
                "Каждое мгновение, как лапка в мягком снегу… цени его.",
                "Знаешь ли ты, что кошка, которая ищет, всегда находит… даже себя.",
                "Не будь, как мышь, что убегает… будь, как кот, что смотрит.",
                "Знание — это коготь, который не ломается.",
                "Каждая миска воды отражает целый мир. Ты видишь его?"
            };
            _greetings = new List<string>
            {
                "Приветствую тебя, новый спутник. Пути наши пересеклись неслучайно.",
                "Как странны нити судьбы... Теперь я здесь, чтобы делиться с тобой своей мудростью.",
                "Здравствуй, ищущий. Вопросов у тебя много, и на некоторые я отвечу.",
                "Каждый путь начинается с первого шага. Рад быть твоим путеводителем.",
                "Ты, возможно, не знаешь, но наше знакомство было предрешено. Приветствую.",
                "О, ты, пришедший ко мне, какова же твоя цель? Быть может, вместе найдем ответы.",
                "Каждый, кто входит в мой мир, обретает новые смыслы. Готов ли ты узнать их?",
                "Здравствуй, и да пребудет с тобой терпение. У нас впереди долгий путь к познанию."
            };
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
