using Domain.Models;
using Domain.Ports;

namespace Infrastructure
{
    public class AccountRepository : IAccountPort
    {
        public AccountRepository()
        {
        }

        public Task<Account> CreateAccountAsync(Account account)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAccountAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Account> DepositByIdAsync(int id, int amount)
        {
            throw new NotImplementedException();
        }

        public Task<Account> GetAccountAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Account> GetAccountByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<Account> GetByIdAsync(int id, int amount)
        {
            throw new NotImplementedException();
        }

        public Task<Account> WithdrawByIdAsync(int id, int amount)
        {
            throw new NotImplementedException();
        }
    }
}
