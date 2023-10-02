using KataBankAccount.Data;
using KataBankAccount.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace KataBankAccount.Repository
{
    public class BankAccountRepository : IBankAccountRepository
    {
        private readonly DbContextClass _dbContext;
        private History _history;

        public BankAccountRepository(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }


        //UPDATE
        public async Task<int> DepositAsync(int id, float amountToAdd)
        {
            var entity = _dbContext.BankAccounts.FirstOrDefaultAsync(x => x.Id == id);

            if(entity != null) 
            {
                var OldBalance = entity.Result.Balance;
                entity.Result.Balance += amountToAdd;
                _history.Description = $"[{DateTime.Now}] Deposit of {amountToAdd}€, old amount : {OldBalance}€, new amount : {entity.Result.Balance}€.";
                entity.Result.TransactionHistory.Add(_history);
                
            }
            return await _dbContext.SaveChangesAsync();
        }


        //UPDATE
        public async Task<int> WithdrawAsync(int id, float amountToSubtract)
        {
            var entity = _dbContext.BankAccounts.FirstOrDefaultAsync(x => x.Id == id);

            if (entity != null)
            {
                var OldBalance = entity.Result.Balance;
                entity.Result.Balance -= amountToSubtract;
                _history.Description = $"[{DateTime.Now}] Withdraw of {amountToSubtract}€, old amount : {OldBalance}€, new amount : {entity.Result.Balance}€.";
                entity.Result.TransactionHistory.Add(_history);

            }
            return await _dbContext.SaveChangesAsync();
        }


        //GET
        public async Task<float> GetBalanceAsync(int id)
        {
            var entity = _dbContext.BankAccounts.FirstOrDefaultAsync(x => x.Id == id);

            if (entity != null)
            {
                _history.Description = $"[{DateTime.Now}] The account balance has been consulted.";
                entity.Result.TransactionHistory.Add(_history);
                return entity.Result.Balance;
            }
            return 0f;
        }


        //GET
        public async Task<string> GetHistoryAsync(int id)
        {
            StringBuilder sb = new StringBuilder();
            var entity = _dbContext.BankAccounts.FirstOrDefaultAsync(x => x.Id == id);

            if (entity != null)
            {
                foreach(var item in entity.Result.TransactionHistory)
                {
                    sb.Append(item.ToString() + "\n");
                }
            }
            return sb.ToString();
        }

        public async Task<BankAccount> GetBankAccountByIdAsync(int id)
        {
            return await _dbContext.BankAccounts.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
