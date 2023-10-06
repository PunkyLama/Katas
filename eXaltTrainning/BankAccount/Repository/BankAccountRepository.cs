using KataBankAccount.Data;
using KataBankAccount.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace KataBankAccount.Repository
{
    public class BankAccountRepository : IBankAccountRepository
    {
        private readonly DbContextClass _dbContext;  
        private TransactionRepository _transactionRepository = new TransactionRepository();

        public BankAccountRepository(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }


        //UPDATE
        public async Task<BankAccount> DepositAsync(int id, float amountToAdd)
        {

            var entity = _dbContext.BankAccounts.Where(x => x.BankAccountId == id).Include(h => h.TransactionHistory).FirstOrDefaultAsync();


            if (entity != null)
            {
                var OldBalance = entity.Result.Balance;
                entity.Result.Balance += amountToAdd;
                var addTransaction = _transactionRepository.AddTransaction(2, entity, amountToAdd, OldBalance);
                entity.Result.TransactionHistory.Add(addTransaction);

            }
            await _dbContext.SaveChangesAsync();
            return entity.Result;
        }


        //UPDATE
        public async Task<BankAccount?> WithdrawAsync(int id, float amountToSubtract)
        {
            var entity = _dbContext.BankAccounts.Where(x => x.BankAccountId == id).Include(h => h.TransactionHistory).FirstOrDefaultAsync();

            if (entity != null)
            {
                var OldBalance = entity.Result.Balance;
                if ((entity.Result.Balance - amountToSubtract) < 0) 
                {
                    return null;
                }
                entity.Result.Balance -= amountToSubtract;
                var addTransaction = _transactionRepository.AddTransaction(3, entity, amountToSubtract, OldBalance);
                entity.Result.TransactionHistory.Add(addTransaction);

            }
            await _dbContext.SaveChangesAsync();
            return entity.Result;
        }


        //GET
        public async Task<float> GetBalanceAsync(int id)
        {
            var entity = _dbContext.BankAccounts.Where(x => x.BankAccountId == id).Include(h => h.TransactionHistory).FirstOrDefaultAsync();

            if (entity != null)
            {
                var addTransaction = _transactionRepository.AddTransaction(4);
                entity.Result.TransactionHistory.Add(addTransaction);
                return entity.Result.Balance;
            }
            return 0f;
        }


        //GET
        public async Task<string> GetHistoryAsync(int id)
        {
            StringBuilder sb = new StringBuilder();
            var entity = _dbContext.BankAccounts.Where(x => x.BankAccountId == id).Include(h => h.TransactionHistory).FirstOrDefaultAsync();

            if (entity != null)
            {
                foreach (var item in entity.Result.TransactionHistory)
                {
                    sb.Append(item.Description.ToString() + "\n");
                }
            }
            return sb.ToString();
        }

        //GET
        public async Task<BankAccount> GetBankAccountByIdAsync(int id)
        {
            //var result= _dbContext.BankAccounts.Where(x => x.Id == id && x.TransactionHistory.Where(y => y.)).FirstOrDefaultAsync();
            var pouet = _dbContext.BankAccounts.Where(x => x.BankAccountId == id).Include(h => h.TransactionHistory).FirstOrDefaultAsync();
            return await pouet;
        }

        //POST
        public async Task<BankAccount> AddBankAccountAsync(AddBankAccount addbankAccount)
        {
            var bankAccount = new BankAccount()
            {
                Name = addbankAccount.Name,
                Balance = addbankAccount.Balance,
                TransactionHistory = new List<Transaction>() { _transactionRepository.AddTransaction(1) }
            };
            var result = _dbContext.BankAccounts.Add(bankAccount);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

    }
}
