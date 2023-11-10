using AutoMapper;
using Domain.Models;
using Domain.Ports.Driven;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace Infrastructure.Respositories
{
    public abstract class BaseAccountPersistencePort<TContext> : IAccountPersistencePort where TContext : DbContext
    {
        private readonly TContext _dbContext;
        private readonly IMapper _mapper;

        protected BaseAccountPersistencePort(TContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); ;
        }

        public virtual async Task<Account> GetAccountByIdAsync(int id)
        {
            var accountEntity =  await _dbContext.Set<AccountEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            var account = _mapper.Map<Account>(accountEntity);
            return account;
        }

        public virtual async Task<List<Statement>> GetStatementsByAccountIdAsync(int accountId, int elements)
        {
            var statementEntity = await _dbContext.Set<StatementEntity>().AsNoTracking().Where(x => x.AccountId == accountId).OrderByDescending(c => c.Date).Take(elements).ToListAsync();
            var statement = _mapper.Map<List<Statement>>(statementEntity);
            return statement;
        }

        public virtual async Task Save(Account account, Statement? transaction)
        {
            if (account != null)
            {
                var accountEntity = _mapper.Map<AccountEntity>(account);
                var dbAccount = await _dbContext.Set<AccountEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == accountEntity.Id);

                if (dbAccount == null)
                {
                    // If the account doesn't exist, add a new instance to the DbContext
                    _dbContext.Set<AccountEntity>().Add(accountEntity);
                } else
                {
                    _dbContext.Set<AccountEntity>().Update(accountEntity);
                }
            }

            if (transaction != null)
            {
                var transactionEntity = _mapper.Map<StatementEntity>(transaction);
                transactionEntity.AccountId = account.Id;
                var dbStatement = await _dbContext.Set<StatementEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == transactionEntity.Id);
                if(dbStatement == null)
                {
                    _dbContext.Set<StatementEntity>().Add(transactionEntity);
                } else
                {
                    _dbContext.Set<StatementEntity>().Update(transactionEntity);
                }
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
