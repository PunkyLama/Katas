using Domain.Models;
using Domain.Ports.Driven;

namespace Repository.Adapters
{
    internal class Persistence : IAccountPersistencePort
    {
        public Task<Account> GetAccountByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Statement>> GetStatementsByAccountIdAsync(int accountId, int elements)
        {
            throw new NotImplementedException();
        }

        public Task Save(Account account, Statement? transaction = null)
        {
            throw new NotImplementedException();
        }
    }
}
