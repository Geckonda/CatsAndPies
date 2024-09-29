using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.Abstractions.Repositories
{
    public interface IBaseRepository<T>
    {
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(int id);
        Task<List<T>?> GetAll();
        Task<T?> GetOneById(int id);
    }
}
