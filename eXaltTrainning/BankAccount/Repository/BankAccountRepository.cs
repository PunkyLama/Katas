using KataBankAccount.Data;
using KataBankAccount.Models;
using System.Text;

namespace KataBankAccount.Repository
{
    public class BankAccountRepository : IBankAccount
    {
        private readonly DbContextClass _dbContext;

        public BankAccountRepository(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        public void Deposit(int id, float amountToAdd)
        {
            var entity = _dbContext.BankAccounts.FirstOrDefault(x => x.Id == id);

            if(entity != null) 
            {
                var OldBalance = entity.Balance;
                entity.Balance += amountToAdd;
                entity.TransactionHistory.Add($"[{DateTime.Now}] Deposit of {amountToAdd}€, old amount : {OldBalance}€, new amount : {entity.Balance}€.");
                
            } 
            _dbContext.SaveChanges();
        }

        public void Withdraw(int id, float amountToSubtract)
        {
            var entity = _dbContext.BankAccounts.FirstOrDefault(x => x.Id == id);

            if (entity != null)
            {
                var OldBalance = entity.Balance;
                entity.Balance -= amountToSubtract;
                entity.TransactionHistory.Add($"[{DateTime.Now}] Withdraw of {amountToSubtract}€, old amount : {OldBalance}€, new amount : {entity.Balance}€.");

            }
            _dbContext.SaveChanges();
        }

        public float Balance(int id)
        {
            var entity = _dbContext.BankAccounts.FirstOrDefault(x => x.Id == id);

            if (entity != null)
            {
                entity.TransactionHistory.Add($"[{DateTime.Now}] The account balance has been consulted.");
                return entity.Balance;
            }
            return 0;
        }

        public string History(int id)
        {
            StringBuilder sb = new StringBuilder();
            var entity = _dbContext.BankAccounts.FirstOrDefault(x => x.Id == id);

            if (entity != null)
            {
                foreach(var item in entity.TransactionHistory)
                {
                    sb.Append(item.ToString() + "\n");
                }
            }
            return sb.ToString();
        }
    }
}
