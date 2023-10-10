using Data;
using Domain.Models;
using Domain.Ports.Driven;
using Domain.Ports.Driving;
using Microsoft.EntityFrameworkCore;
using System.Buffers;

namespace Infrastructure
{
    public class AccountAdapter : IAccountPort, IAccountPersistencePort
    {
        public readonly DbContextBank _dbContext;
        public AccountAdapter(DbContextBank dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Account> DepositByIdAsync(int id, float amount)
        {
            var account = await GetAccountByIdAsync(id);            
            account.Balance += amount;
            TransactionHistory transaction = new TransactionHistory(DateTime.Now, Operation.Deposit, TransactionStatus.Approuved);
            account.TransactionHistories.Add(transaction);
            await _dbContext.SaveChangesAsync();
            return account;
        }

        public async Task<Account> GetAccountByIdAsync(int id)
        {
            var account = await _dbContext.Accounts.Where(x => x.Id == id).Include(h => h.TransactionHistories).FirstOrDefaultAsync();
            return account;
        }

        public async Task<ICollection<TransactionHistory>> GetStatementByIdAsync(int id)
        {
            var transaction = await _dbContext.TransactionHistories.Where(x => x.AccountId == id).ToListAsync();
            return transaction;
        }

        public async Task SaveAccount(int id)
        {
        }

        public async Task<Account> WithdrawByIdAsync(int id, float amount)
        {
            var account = await GetAccountByIdAsync(id);
            TransactionHistory transaction;
            if ((account.Balance - amount) < 0)
            {
                transaction = new TransactionHistory(DateTime.Now, Operation.Withdraw, TransactionStatus.Rejected);
            }
            else
            {
                account.Balance -= amount;
                transaction = new TransactionHistory(DateTime.Now, Operation.Withdraw, TransactionStatus.Approuved);
            }
            account.TransactionHistories.Add(transaction);
            await _dbContext.SaveChangesAsync();
            return account;
        }
    }
}