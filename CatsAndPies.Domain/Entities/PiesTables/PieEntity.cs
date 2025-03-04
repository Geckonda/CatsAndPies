using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.Entities.PiesTables
{
    public class PieEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTime Created { get; set; }
        public string ImgLink { get; set; } = string.Empty;

        public int EffectId { get; set; }
        public PieEffectEntity Effect { get; set; }
        public int OwnerId { get; set; }
        public UserEntity Owner { get; set; }
    }
}
