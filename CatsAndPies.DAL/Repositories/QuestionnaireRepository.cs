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
    public class QuestionnaireRepository : IBaseRepository<QuestionnaireEntity>
    {
        private readonly AppDbContext _db;
        public QuestionnaireRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task Add(QuestionnaireEntity entity)
        {
            await _db.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            await _db.Questionnairies
                .Where(q => q.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<List<QuestionnaireEntity>?> GetAll()
        {
            return await _db.Questionnairies
                .Include(q => q.User)
                .ToListAsync();
        }

        public async Task<QuestionnaireEntity?> GetOneById(int id)
        {
            return await _db.Questionnairies
                .Include (q => q.User)
                .FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task Update(QuestionnaireEntity entity)
        {
            await _db.Questionnairies
                .Where(q => q.Id == entity.Id)
                .ExecuteUpdateAsync(q => q
                    .SetProperty(q => q.Name, entity.Name)
                    .SetProperty(q => q.Birthday, entity.Birthday)
                    .SetProperty(q => q.Hobby, entity.Hobby)
                    .SetProperty(q => q.Season, entity.Season)
                    .SetProperty(q => q.Flower, entity.Flower)
                    .SetProperty(q => q.Dish, entity.Dish)
                    .SetProperty(q => q.ChillTime, entity.ChillTime)
                    .SetProperty(q => q.Film, entity.Film)
                    .SetProperty(q => q.Singer, entity.Singer)
                    .SetProperty(q => q.Color, entity.Color)
                    .SetProperty(q => q.PositiveTraits, entity.PositiveTraits)
                    .SetProperty(q => q.Dream, entity.Dream));
        }
    }
}
