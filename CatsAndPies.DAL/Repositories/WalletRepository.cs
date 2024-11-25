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
    public class WalletRepository : IWalletRepository
    {
        private readonly AppDbContext _db;
        public WalletRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task Add(WalletEntity entity)
        {
            await _db.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            await _db.Wallets
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<decimal> GetBalanceByUserId(int userId)
        {
            return await _db.Wallets
                .Where(x => x.UserId == userId)
                .Select(x => x.Balance)
                .FirstOrDefaultAsync();
        }

        public async Task<WalletEntity?> GetOneById(int id)
        {
            return await _db.Wallets
                .Where (x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task Update(WalletEntity entity)
        {
            await _db.Wallets
                .Where(x => x.Id == entity.Id)
                .ExecuteUpdateAsync(x =>
                    x.SetProperty(p => p.Balance , entity.Balance));
        }
    }
}
