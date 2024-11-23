using CatsAndPies.Domain.Abstractions.Helpers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.Helpres.Cat
{
    public class JsonPersonalityDataProvider : IPersonalityDataProvider
    {
        public List<string> GetGreetings(string personalityType)
        {
            var data = LoadPersonalityData(personalityType);
            return data["responses"];
        }

        public List<string> GetResponses(string personalityType)
        {
            var data = LoadPersonalityData(personalityType);
            return data["greetings"];
        }
        private Dictionary<string, List<string>> LoadPersonalityData(string personalityType)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Cat", "PersonalityData", $"{personalityType}.json");

            // Проверьте, существует ли файл
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Файл {filePath} не найден.");
            }

            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<Dictionary<string, List<string>>>(json)
                   ?? new Dictionary<string, List<string>>();
        }
    }
}
