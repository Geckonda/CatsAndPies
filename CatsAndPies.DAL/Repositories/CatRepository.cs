﻿using CatsAndPies.Domain.Abstractions.Repositories.Combined;
using CatsAndPies.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.DAL.Repositories
{
    public class CatRepository : ICatRepository
    {
        private readonly AppDbContext _db;
        public CatRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task Add(CatEntity entity)
        {
            await _db.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            await _db.Cats
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();
        }


        public Task<CatEntity?> GetOneById(int id)
        {
            return _db.Cats
                .Include(x => x.Color)
                .Include(x => x.Personality)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<CatEntity?> GetOneByUserId(int userId)
        {
            return _db.Cats
                .Include(x => x.Color)
                .Include(x => x.Personality)
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }

        /// <summary>
        /// Метод получает случайный цвет и характер из БД
        /// </summary>
        /// <returns>color, personality</returns>
        public async Task<(int, int)> GetRandomColorAndPersonality()
        {
            var color = await _db.CatsColors
                .OrderBy(c => Guid.NewGuid())
                .Select(c => c.Id)
                .FirstOrDefaultAsync();
            
            var personality = await _db.CatsPersonalities
                .OrderBy(p => Guid.NewGuid())
                .Select(p => p.Id)
                .FirstOrDefaultAsync();
            return (color, personality);
        }

        public async Task Update(CatEntity entity)
        {
            await _db.Cats
                .Where(q => q.Id == entity.Id)
                .ExecuteUpdateAsync(q => q
                    .SetProperty(q => q.Name, entity.Name)
                    .SetProperty(q => q.AdoptedTime, entity.AdoptedTime)
                    .SetProperty(q => q.ColorId, entity.ColorId)
                    .SetProperty(q => q.PersonalityId, entity.PersonalityId));
        }
    }
}
