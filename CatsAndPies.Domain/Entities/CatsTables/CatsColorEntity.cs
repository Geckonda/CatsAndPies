using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.Entities.Cats
{
    public class CatsColorEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string HexColor { get; set; }
        [JsonIgnore]
        public List<CatEntity> Cats { get; set; }
    }
}
