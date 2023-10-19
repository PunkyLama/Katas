using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            account.Balance += amount;
            TransactionHistory transaction = new TransactionHistory(DateTime.Now, Operation.Deposit, TransactionStatus.Approuved);
            account.TransactionHistories.Add(transaction);
            await _persistencePort.SaveAccount();
            return account;
        }

        public async Task<ICollection<TransactionHistory>> GetStatementByIdAsync(int id)
        {
            var account = await _persistencePort.GetAccountByIdAsync(id);
            if (account == null)
            {
                return default;
            }
            return account.TransactionHistories;
        }

        public async Task<Account> WithdrawByIdAsync(int id, float amount)
        {
            var account = await _persistencePort.GetAccountByIdAsync(id);
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
            await _persistencePort.SaveAccount();
            return account;
        }
    }
}
