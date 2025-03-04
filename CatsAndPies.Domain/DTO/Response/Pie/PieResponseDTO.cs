using CatsAndPies.Domain.Entities.PiesTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.DTO.Response.Pie
{
    public class PieResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTime Created { get; set; }
        public string ImgLink { get; set; } = string.Empty;


        public PieEffectResponseDTO Effect { get; set; }
        public PieRarityResponseDTO Rarity { get; set; }
    }
}
