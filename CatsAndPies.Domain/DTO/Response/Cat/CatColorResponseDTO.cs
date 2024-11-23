using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.DTO.Response.Cat
{
    public class CatColorResponseDTO
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string HexColor { get; set; }
    }
}
