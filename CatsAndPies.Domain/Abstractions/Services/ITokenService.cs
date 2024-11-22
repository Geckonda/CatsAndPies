using CatsAndPies.Domain.Entities;
using CatsAndPies.Domain.Helpres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.Abstractions.Services
{
    public interface ITokenService
    {
        public TokenResult GenerateJwtToken(UserEntity user);
    }
}
