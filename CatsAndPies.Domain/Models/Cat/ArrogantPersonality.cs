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

        public ArrogantPersonality()
        {
            _responses = new List<string>
            {
                "Ты ведь понимаешь, что я тут главный, да?",
                "Даже пытаться не стоит, у тебя не получится так, как у меня.",
                "Ах, люди… всегда такие наивные.",
                "Мои когти точнее твоих слов… Не то чтобы я хвастаюсь.",
                "Ох, неужели ты думал, что меня этим удивишь? Серьезно?",
                "Интересно, как вы там, без моего гения, вообще справляетесь?",
                "Ты сделал это… о, как интересно… для новичка, конечно."
            };
            _greetings = new List<string>
            {
                "Ты привилегирован быть моим новым хозяином... Надеюсь, ты это понимаешь.",
                "Ну, я позволю тебе быть рядом со мной. Считай это честью.",
                "Позволь представиться: я — твое лучшее приобретение.",
                "Вижу, ты старался меня заслужить. Так и быть, приму тебя.",
                "Что ж, надеюсь, ты достаточно хорош для моего внимания.",
                "Ну, а ты не так уж плох. Может, привыкну к тебе со временем.",
                "Так ты теперь мой хозяин? Ладно, посмотрим, сможешь ли ты заслужить эту роль.",
                "Не знаю, чем ты заслужил такую удачу, но теперь я здесь... Считай себя особенным."
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
