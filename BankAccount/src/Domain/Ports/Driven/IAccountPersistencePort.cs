using Domain.Models;

namespace Domain.Ports.Driven
{
    public interface IAccountPersistencePort
    {
        public Task<Account> GetAccountByIdAsync(int id);
        public Task SaveAccount(Account account);
    }
}
