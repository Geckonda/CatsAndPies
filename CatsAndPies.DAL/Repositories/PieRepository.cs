using CatsAndPies.Domain.Abstractions.Repositories;
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
    public class PieRepository : IPieRepository
    {
        private readonly AppDbContext _db;
        public PieRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task Add(PieEntity entity)
        {
            await _db.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            await _db.Pies
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<List<PieEntity>> GetAll()
        {
            return await _db.Pies.ToListAsync();
        }

        public async Task<PieEntity?> GetOneById(int id)
        {
            return await _db.Pies.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<PieEntity>> GetUserPies(int userId)
        {
            return await _db.Pies
                .Where(x => x.Owner.Id == userId)
                .ToListAsync();
        }

        public async Task Update(PieEntity entity)
        {
           await _db.Pies
                .Where(x => x.Id == entity.Id)
                .ExecuteUpdateAsync(x => x
                    .SetProperty(p => p.Name, entity.Name)
                    .SetProperty(p => p.Description, entity.Description)
                    .SetProperty(p => p.Price, entity.Price)
                    .SetProperty(p => p.ImgLink, entity.ImgLink));
        }
    }
}
