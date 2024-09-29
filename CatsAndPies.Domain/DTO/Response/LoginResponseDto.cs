using CatsAndPies.Domain.Helpres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.DTO.Response
{
    public class LoginResponseDto
    {
        public string Name {  get; set; }
        public string Login { get; set; }
        public TokenResult Token { get; set; }
    }
}
