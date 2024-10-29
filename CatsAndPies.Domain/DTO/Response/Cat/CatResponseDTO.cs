using CatsAndPies.Domain.Entities.Cats;
using CatsAndPies.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.DTO.Response.Cat
{
    public class CatResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime AdoptedTime { get; set; }

        public CatOwnerResponseDTO Owner { get; set; }
        public CatColorResponseDTO Color { get; set; }
        public CatPersonalityResponseDTO Personality { get; set; }
        
        public string Phrase { get; set; }  = string.Empty;
    }
}
