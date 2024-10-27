using CatsAndPies.Domain.Entities.Cats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.Entities
{
    public class CatEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ColorId { get; set; }
        public int PersonalityId { get; set; }
        public string Name { get; set; }
        public DateTime AdoptedTime { get; set; }

        public UserEntity Owner { get; set; }
        public CatsColorEntity Color { get; set; }
        public CatsPersonalityEntity Personality { get; set; }
    }
}
