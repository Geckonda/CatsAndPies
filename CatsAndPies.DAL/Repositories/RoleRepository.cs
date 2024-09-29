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
    public class RoleRepository : IBaseRepository<RoleEntity>
    {
        private readonly AppDbContext _db;
        public RoleRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task Add(RoleEntity entity)
        {
            await _db.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            await _db.Roles
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<List<RoleEntity>?> GetAll()
        {
            return await _db.Roles
                .ToListAsync();
        }

        public async Task<RoleEntity?> GetOneById(int id)
        {
            return await _db.Roles
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Update(RoleEntity entity)
        {
            await _db.Roles
                .Where(x => x.Id == entity.Id)
                .ExecuteUpdateAsync(x => x
                    .SetProperty(p => p.Name , entity.Name));
        }
    }
}
