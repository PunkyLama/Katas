using Data;
using Domain.Models;
using Domain.Ports.Driven;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class AccountAdapter : IAccountPersistencePort
    {
        public readonly DbContextBank _dbContext;
        public AccountAdapter(DbContextBank dbContext)
        {
            _dbContext = dbContext;
        }


        // Faire Mapper entre domaine et infra puis WEBAPI
        public async Task<Account> GetAccountByIdAsync(int id)
        {
            var account = await _dbContext.Accounts.Where(x => x.Id == id).Include(h => h.TransactionHistories).FirstOrDefaultAsync();
            return account;
        }
        public async Task SaveAccount()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}