using Domain.Models;
using Domain.Ports.Driven;
using Infrastructure.Services;
using Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Infrastructure.Adapters
{
    public class InfrastructureAccountAdapter : IAccountPersistencePort
    {
        public readonly DbContextBank _dbContext;
        private AccountMapper mapper = new AccountMapper();
        public InfrastructureAccountAdapter(DbContextBank dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Account> GetAccountByIdAsync(int id)
        {
            var account = await _dbContext.Accounts.Where(x => x.Id == id).Include(h => h.TransactionHistories).FirstOrDefaultAsync();
            if (account == null)
            {
                return default;
            }
            var domainAccount = mapper.MapFrom(account);
            return domainAccount;
        }
        public async Task SaveAccount(Account account)
        {
            var domainAccount = mapper.MapTo(account);
            var dbAccount = await _dbContext.Accounts.Where(x => x.Id == domainAccount.Id).Include(h => h.TransactionHistories).FirstOrDefaultAsync();
            _dbContext.Entry(dbAccount).CurrentValues.SetValues(domainAccount);
            foreach (var transaction in domainAccount.TransactionHistories)
            {
                if(transaction.Id == 0)
                {
                    _dbContext.Entry(transaction).State = EntityState.Added;
                }                
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}


