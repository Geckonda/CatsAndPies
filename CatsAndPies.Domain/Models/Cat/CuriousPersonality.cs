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

        public CuriousPersonality()
        {
            _responses = new List<string>
            {
                "А что это? Скажи-ка, пожалуйста!",
                "Ого! Ты видел это?! Это… это что-то невероятное!",
                "Сколько всего интересного вокруг! Ты знаешь, как оно работает? Я хочу узнать!",
                "Эй, эй, подожди! А зачем ты это делаешь?",
                "Ух ты, это ново для меня! А для чего это нужно?",
                "Хмм… а ты когда-нибудь задумывался, как работает эта штука? наклоняет голову",
                "А что будет, если я трону вот это…? Ой, можно, можно?",
                "Сколько открытий! Какой удивительный день!",
            };
            _greetings = new()
            {
                "О! А ты кто такой? Ты меня кормить будешь? Где ты был раньше?",
                "Привет-привет! О, у тебя вкусно пахнет! Это для меня?",
                "Ого! А ты кто? И что у тебя там в руке? Это съедобное? Вроде мышка.",
                "Привет! О, а ты тоже любишь шуршать бумажками? Это так весело!",
                "Ой, смотри! Новый человек! У тебя, случайно, нет игрушек для меня?",
                "О! А ты сможешь мне почесать за ушком? Там, где я не достаю?",
                "Ты тоже любишь лазить по коробкам? Это так интересно!",
                "Ого, а ты раньше здесь был? Или ты новенький, как я?",
                "О, ты теперь со мной? Здорово! Слушай, а у тебя что за лапы такие странные?"
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
        //Заглушка
        public override string GenerateRandomMessage()
        {
            return _responses[_random.Next(_responses.Count)];
        }
    }
}
