using CatsAndPies.Domain.Abstractions.Repositories;
using CatsAndPies.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.DAL.Repositories
{
    public class ExceptionLogRepository : IBaseRepository<ExceptionLogEntity>
    {
        private readonly AppDbContext _db;
        public ExceptionLogRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task Add(ExceptionLogEntity entity)
        {
            await _db.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            await _db.Errors.Where(x => x.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<ExceptionLogEntity?> GetOneById(int id)
        {
            return await _db.Errors.Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task Update(ExceptionLogEntity entity)
        {
            await _db.Errors.Where(x => x.Id == entity.Id)
                .ExecuteUpdateAsync(x => x
                .SetProperty(p => p.ExceptionTime, entity.ExceptionTime)
                .SetProperty(p => p.ExceptionType, entity.ExceptionType)
                .SetProperty(p => p.Method, entity.Method)
                .SetProperty(p => p.ExceptionText, entity.ExceptionText));
        }
    }
}
