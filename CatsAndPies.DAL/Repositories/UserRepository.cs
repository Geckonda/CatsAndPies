using CatsAndPies.Domain.Abstractions.Repositories;
using CatsAndPies.Domain.Abstractions.Repositories.Combined;
using CatsAndPies.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;
        public UserRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task Add(UserEntity entity)
        {
            await _db.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            await _db.Users
                .Where(u => u.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<UserEntity?> GetOneById(int id)
        {
            return await _db.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Id ==  id);

        }

        public async Task<UserEntity?> GetOneByLogin(string login)
        {
            return await _db.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x =>  login == x.Login);
        }

        public async Task Update(UserEntity entity)
        {
            await _db.Users
                .Where(x => x.Id == entity.Id)
                .ExecuteUpdateAsync(x => x
                    .SetProperty(user => user.Name, entity.Name)
                    .SetProperty(user => user.Password, entity.Password));
        }
    }
}
