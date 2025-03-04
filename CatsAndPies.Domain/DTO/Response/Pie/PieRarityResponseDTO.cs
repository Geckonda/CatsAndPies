using CatsAndPies.Domain.Entities.PiesTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.DTO.Response.Pie
{
    public class PieRarityResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Chance { get; set; }
    }
}
