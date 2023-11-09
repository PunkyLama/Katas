using Infrastructure.Context;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Domain.Ports.Driven;
using Domain.Models;
using Domain.Exceptions;

namespace Infrastructure.Adapters
{
    public class InfrastructureAccountAdapter : IAccountPersistencePort
    {
        public readonly DbContextBank _dbContext;
        private readonly IMapper _mapper;
        public InfrastructureAccountAdapter(DbContextBank dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Account> GetAccountByIdAsync(int id)
        {
            var account = await _dbContext.Accounts.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (account == null)
            {
                throw new AccountNotFound(id);
            }
            return _mapper.Map<Account>(account);
        }

        public async Task<List<Statement>> GetStatementsByAccountIdAsync(int accountId, int elements)
        {
            List<StatementEntity> statementEntity = await _dbContext.TransactionHistories.Where(x => x.AccountId == accountId).OrderByDescending(c => c.Date).Take(elements).ToListAsync();
            List<Statement> statements = new List<Statement>();
            if (statementEntity == null)
            {
                throw new AccountNotFound(accountId);
            }

            foreach (var transaction in statementEntity)
            {
                if (transaction == null || transaction.AccountId != accountId)
                {
                    throw new AccountNotFound(accountId);
                }
                statements.Add(_mapper.Map<Statement>(transaction));
            }
            
            return statements;
        }

        public async Task Save(Account account, Statement? transaction)
        {
            if (account != null)
            {
                var domainAccount = _mapper.Map<AccountEntity>(account);
                var dbAccount = await _dbContext.Accounts.FirstOrDefaultAsync(x => x.Id == domainAccount.Id);

                if (dbAccount == null)
                {
                    // If the account doesn't exist, add it to the DbContext
                    _dbContext.Accounts.Add(domainAccount);
                }
                else
                {
                    // If the account already exists, update its values
                    _dbContext.Entry(dbAccount).CurrentValues.SetValues(domainAccount);
                }
            }

            if (transaction != null)
            {
                var domainTransaction = _mapper.Map<StatementEntity>(transaction);
                domainTransaction.AccountId = account.Id;
                var dbTransaction = await _dbContext.TransactionHistories.FirstOrDefaultAsync(x => x.Id == domainTransaction.Id);

                if (dbTransaction == null)
                {
                    // If the transaction doesn't exist, add it to the DbContext
                    _dbContext.TransactionHistories.Add(domainTransaction);
                }
                else
                {
                    // If the transaction already exists, update its values
                    _dbContext.Entry(dbTransaction).CurrentValues.SetValues(domainTransaction);
                }
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}


