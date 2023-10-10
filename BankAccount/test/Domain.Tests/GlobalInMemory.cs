using Domain.Models;

namespace Domain.Tests
{
    public class GlobalInMemory
    {     
        public DbContextOptions<DbContextBank> _options { get; set; }
        public GlobalInMemory() 
        {
            _options = new DbContextOptionsBuilder<DbContextBank>()
                .UseInMemoryDatabase(databaseName: "BankAccountTest")
                .Options;
            InitializeData();
        }
        private void InitializeData ()
        {
            using (var context = new DbContextBank(_options))
            {
                var account = new Account
                {
                    Id = 1,
                    Balance = 100,
                    TransactionHistories = new List<TransactionHistory>()
                };
                context.Accounts.Add(account);
                context.SaveChanges();
            }
        }
    }
}