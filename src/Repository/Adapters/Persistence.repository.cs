using Domain.Models;
using Domain.Ports.Driven;
using Microsoft.EntityFrameworkCore;


namespace Repository.Adapters
{
    public abstract class BaseAccountPersistencePort<TContext> : IAccountPersistencePort where TContext : DbContext
    {
        private readonly TContext _dbContext;

        protected BaseAccountPersistencePort(TContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public virtual async Task<Account> GetAccountByIdAsync(int id)
        {
            // Implémentez la logique spécifique à la récupération du compte dans la base de données
            return await _dbContext.Set<Account>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<List<Statement>> GetStatementsByAccountIdAsync(int accountId, int elements)
        {
            // Implémentez la logique spécifique à la récupération des transactions dans la base de données
            return await _dbContext.Set<Statement>().Where(x => x.AccountId == accountId).OrderByDescending(c => c.Date).Take(elements).ToListAsync();
        }

        public virtual async Task Save(Account account, Statement? transaction)
        {
            if (account != null)
            {
                // Implémentez la logique spécifique à la sauvegarde du compte dans la base de données
                _dbContext.Set<Account>().Update(account);
            }

            if (transaction != null)
            {
                // Implémentez la logique spécifique à la sauvegarde de la transaction dans la base de données
                _dbContext.Set<Statement>().Update(transaction);
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
