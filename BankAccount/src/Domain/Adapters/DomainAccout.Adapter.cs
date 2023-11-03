namespace Domain.Adapters
{
    public class DomainAccoutAdapter : IAccountPort
    {
        private readonly IAccountPersistencePort _persistencePort;

        public DomainAccoutAdapter(IAccountPersistencePort persistencePort)
        {
            _persistencePort = persistencePort;
        }

        public async Task<Account> DepositByIdAsync(int id, float amount)
        {
            var account = await _persistencePort.GetAccountByIdAsync(id);
            TransactionHistory transaction;
            if (account == null)
            {
                throw new Exception("Account not found");
            }
            if (amount <= 0)
            {
                transaction = new TransactionHistory(account.Id, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), Operation.Deposit, TransactionStatus.Rejected, amount, account.Balance);
            }
            else
            {
                var oldBalance = account.Balance;
                account.Balance += amount;
                transaction = new TransactionHistory(account.Id, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), Operation.Deposit, TransactionStatus.Approuved, amount, oldBalance, account.Balance);
            }
            account.TransactionHistories.Add(transaction);
            await _persistencePort.SaveAccount(account);
            return account;
        }

        public async Task<float> GetBalanceAsync(int id)
        {
            var account = await _persistencePort.GetAccountByIdAsync(id);
            if (account == null)
            {
                throw new Exception("Account not found");
            }
            return account.Balance;
        }

        public async Task<ICollection<TransactionHistory>> GetStatementByIdAsync(int id)
        {
            var account = await _persistencePort.GetAccountByIdAsync(id);
            if (account == null)
            {
                throw new Exception("Account not found");
            }
            return account.TransactionHistories;
        }

        public async Task<Account> WithdrawByIdAsync(int id, float amount)
        {
            var account = await _persistencePort.GetAccountByIdAsync(id);
            TransactionHistory transaction;
            if (account == null)
            {
                throw new Exception("Account not found");
            }
            if ((account.Balance - amount) < 0 || amount <= 0)
            {
                transaction = new TransactionHistory(account.Id, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), Operation.Withdraw, TransactionStatus.Rejected, amount, account.Balance);
            }
            else
            {
                var oldBalance = account.Balance;
                account.Balance -= amount;
                transaction = new TransactionHistory(account.Id, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), Operation.Withdraw, TransactionStatus.Approuved, amount, oldBalance, account.Balance);
            }
            account.TransactionHistories.Add(transaction);
            await _persistencePort.SaveAccount(account);
            return account;
        }
    }
}
