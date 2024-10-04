using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.DTO.Response
{
    public class QuestionnaireResponseDto
    {
        public string Name { get; set; } = string.Empty;
        public DateTime Birthday { get; set; }
        public string Hobby { get; set; } = string.Empty;
        public string Season { get; set; } = string.Empty;
        public string Flower { get; set; } = string.Empty;
        public string Dish { get; set; } = string.Empty;
        public string ChillTime { get; set; } = string.Empty;
        public string Film { get; set; } = string.Empty;
        public string Singer { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string PositiveTraits { get; set; } = string.Empty;
        public string Dream { get; set; } = string.Empty;
    }
}
