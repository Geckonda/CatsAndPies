using CatsAndPies.Domain.Entities.PiesTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.Abstractions.Repositories.Combined
{
    public interface IPieRepository : IBaseRepository<PieEntity>
    {
        Task<List<PieEntity>> GetAll();
        Task<List<PieEntity>> GetUserPies(int userId);
        
    }
}
