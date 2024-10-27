using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.Entities.Cats
{
    public class CatsPersonalityEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<CatEntity> Cats { get; set; }
    }
}
