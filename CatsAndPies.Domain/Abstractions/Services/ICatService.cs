using CatsAndPies.Domain.Abstractions.Services.Cat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.Abstractions.Services
{
    public interface ICatService : ICatCreationService, ICatMessageService, ICatQueryService
    {
    }
}
