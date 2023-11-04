using Domain.Models;
using Domain.Ports.Driven;
using Infrastructure.Context;
using Infrastructure.Entities;
using Infrastructure.Mapper;
using Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Infrastructure.Adapters
{
    public class InfrastructureAccountAdapter : IAccountPersistencePort
    {
        public readonly DbContextBank _dbContext;
        private AccountInfraMapper _accountMapper = new AccountInfraMapper();
        private StatementInfraMapper _transactionMapper = new StatementInfraMapper();
        public InfrastructureAccountAdapter(DbContextBank dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Account> GetAccountByIdAsync(int id)
        {
            var account = await _dbContext.Accounts.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (account == null)
            {
                throw new Exception("Account not found");
            }
            var domainAccount = _accountMapper.MapFrom(account);
            return domainAccount;
        }

        public async Task<List<Statement>> GetStatementsByAccountIdAsync(int accountId, int elements)
        {
            List<StatementEntity> transactions = await _dbContext.TransactionHistories.Where(x => x.AccountId == accountId).Skip(Math.Max(0, _dbContext.TransactionHistories.Count() - elements)).ToListAsync();
            List<Statement> domainTransaction = new List<Statement>();
            if (transactions == null)
            {
                throw new Exception("Statements not found");
            }

            foreach (var transaction in transactions)
            {
                domainTransaction.Add(_transactionMapper.MapFrom(transaction));
            }
            
            return domainTransaction;
        }

        public async Task Save(Account account, Statement? transaction)
        {
            if (account != null)
            {
                var domainAccount = _accountMapper.MapTo(account);
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
                var domainTransaction = _transactionMapper.MapTo(transaction);
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


