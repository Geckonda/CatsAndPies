using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.Entities.PiesTables
{
    public class PieEffectEntity
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public int RarityId { get; set; }
        public RarityEntity? Rarity { get; set; }
        public List<PieEntity> Pies { get; set; } = new();
    }
}
