using CatsAndPies.Domain.Abstractions.Repositories.Combined;
using CatsAndPies.Domain.Entities.PiesTables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.DAL.Repositories
{
    public class RarityRepository : IRarityRepository
    {
        private readonly AppDbContext _db;

        public RarityRepository(AppDbContext db)
        {
            _db = db;
        }
        public  Task<List<RarityEntity>> GetAll()
        {
            return _db.Rarities.ToListAsync();
        }
    }
}
