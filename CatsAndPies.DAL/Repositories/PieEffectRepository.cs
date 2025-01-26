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
    public class PieEffectRepository : IPieEffectRepository
    {
        private readonly AppDbContext _db;
        public PieEffectRepository(AppDbContext db)
        {
            _db = db;
        }
        public Task<List<PieEffectEntity>> GetAll()
        {
            return _db.PiesEffects.ToListAsync();
        }
    }
}
