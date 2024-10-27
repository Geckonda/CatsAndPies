using CatsAndPies.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.Abstractions.Repositories.Combined
{
    public interface ICatRepository : IBaseRepository<CatEntity>, IUserOwnedRepository<CatEntity>
    {
    }
}
