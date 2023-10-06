using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    internal interface IAccountPort
    {
        public Task<Account> CreateAccountAsync(Account account);
        public Task<Account> GetAccountAsync(int id);
        public Task<Account> GetAccountByUserIdAsync(int userId);
        public Task<Account> DepositByIdAsync(int id, int amount);
        public Task<Account> WithdrawByIdAsync(int id, int amount);
        public Task<Account> GetByIdAsync(int id, int amount);
        public Task DeleteAccountAsync(int id);
    }
}
