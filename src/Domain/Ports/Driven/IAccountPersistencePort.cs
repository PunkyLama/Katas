namespace Domain.Ports.Driven
{
    public interface IAccountPersistencePort
    {
        public Task<Account> GetAccountByIdAsync(int id);
        public Task<List<Statement>> GetStatementsByAccountIdAsync(int accountId, int elements);
        public Task Save(Account account, Statement? transaction);
    }
}
