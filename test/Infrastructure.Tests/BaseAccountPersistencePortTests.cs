using AutoMapper;
using Domain.Models;
using Infrastructure.Context;
using Infrastructure.Entities;
using Infrastructure.Respositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Infrastructure.Profiles;
using Microsoft.Identity.Client;

namespace Infrastructure.Tests
{
    public class BaseAccountPersistencePortTests
    {
        [Fact]
        public async Task GetAccountByIdAsync_ShouldReturnCorrectAccount()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DbContextBank>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new DbContextBank(options))
            {
                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AccountEntityProfile>()));
                var accountEntity = new AccountEntity { Id = 1, Balance = 100 };
                context.Set<AccountEntity>().Add(accountEntity);
                await context.SaveChangesAsync();
                var repository = new TestAccountPersistencePort(context, mapper);

                // Act
                var result = await repository.GetAccountByIdAsync(1);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(1, result.Id);
                Assert.IsType<Account>(result);
            }
        }

        [Fact]
        public async Task GetStatementsByAccountIdAsync_ShouldReturnCorrectStatements()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DbContextBank>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new DbContextBank(options))
            {
                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<StatementEntityProfile>()));
                var accountId = 1;
                var statementEntities = new List<StatementEntity>
                {
                    new StatementEntity { AccountId = accountId, Amount = 100, Date = DateTime.Now, Id = 1, 
                        Operation = OperationEntity.Deposit, TransactionStatus = StatementSatusEntity.Approved, 
                        NewBalance = 100, OldBalance = 0 },
                    new StatementEntity { AccountId = accountId, Amount = 50, Date = DateTime.Now, Id = 2,
                        Operation = OperationEntity.Withdraw, TransactionStatus = StatementSatusEntity.Approved,
                        NewBalance = 50, OldBalance = 100 },
                };
                context.Set<StatementEntity>().AddRange(statementEntities);
                await context.SaveChangesAsync();
                var repository = new TestAccountPersistencePort(context, mapper);

                // Act
                var result = await repository.GetStatementsByAccountIdAsync(accountId, 2);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(2, result.Count);
                // Add more assertions based on your business logic
            }
        }

        [Fact]
        public async Task Save_ShouldAddOrUpdateAccountAndTransaction()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DbContextBank>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new DbContextBank(options))
            {
                var mapperConfiguration = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<AccountEntityProfile>();
                    cfg.AddProfile<StatementEntityProfile>();
                });

                var mapper = new Mapper(mapperConfiguration);
                var account = new Account { Id = 1, Balance = 100 };
                var transaction = new Statement
                {
                    Amount = 100,
                    Date = DateTime.Now,
                    Id = 1,
                    Operation = Operation.Deposit,
                    StatementStatus = StatementStatus.Approuved,
                    NewBalance = 100,
                    OldBalance = 0
                };
                var repository = new TestAccountPersistencePort(context, mapper);

                // Act
                await repository.Save(account, transaction);

                // Assert
                var dbAccount = await context.Set<AccountEntity>().FirstOrDefaultAsync(x => x.Id == account.Id);
                Assert.NotNull(dbAccount);
                // Add more assertions for the account entity

                var dbTransaction = await context.Set<StatementEntity>().FirstOrDefaultAsync(x => x.Id == transaction.Id);
                Assert.NotNull(dbTransaction);
                // Add more assertions for the transaction entity
            }
        }
    }
    public class TestAccountPersistencePort : BaseAccountPersistencePort<DbContextBank>
    {
        public TestAccountPersistencePort(DbContextBank dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }
    }
}