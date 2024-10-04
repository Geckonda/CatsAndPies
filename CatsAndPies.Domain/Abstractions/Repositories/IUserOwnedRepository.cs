using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.Abstractions.Repositories
{
    public interface IUserOwnedRepository<T>
    {
        Task<T?> GetOneByUserId(int userId);
    }
}
