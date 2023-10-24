using Infrastructure.Adapters;
using Infrastructure.Entities;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Infrastructure.Test
{
    public class InfrastructureAccountAdapterTests
    {
        public DbContextBank InitializeData ()
        {
            var accountsData = new List<AccountEntity>
            {
                new AccountEntity { Id = 1, Balance = 100, TransactionHistories = new List<TransactionHistoryEntity>() },
                new AccountEntity { Id = 2, Balance = 200, TransactionHistories = new List<TransactionHistoryEntity>() }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<AccountEntity>>();
            mockDbSet.As<IQueryable<AccountEntity>>().Setup(m => m.Provider).Returns(accountsData.Provider);
            mockDbSet.As<IQueryable<AccountEntity>>().Setup(m => m.Expression).Returns(accountsData.Expression);
            mockDbSet.As<IQueryable<AccountEntity>>().Setup(m => m.ElementType).Returns(accountsData.ElementType);
            mockDbSet.As<IQueryable<AccountEntity>>().Setup(m => m.GetEnumerator()).Returns(accountsData.GetEnumerator());

            var mockDbContext = new Mock<DbContextBank>();
            mockDbContext.Setup(c => c.Accounts).Returns(mockDbSet.Object);

            return mockDbContext.Object;
        }


        [Fact]
        public async Task GetAccountByIdAsync_ShouldReturnCorrectAccount()
        {
            // Arrange
            var adapter = new InfrastructureAccountAdapter(InitializeData());

            // Act
            var account = await adapter.GetAccountByIdAsync(1);

            // Assert
            Assert.NotNull(account);
            Assert.Equal(1, account.Id);
            Assert.Equal(100, account.Balance);
        }

        [Fact]
        public async Task GetAccountByIdAsync_ShouldReturnNullForNonExistingAccount()
        {
            // Arrange
            var adapter = new InfrastructureAccountAdapter(InitializeData());

            // Act
            var account = await adapter.GetAccountByIdAsync(0);

            // Assert
            Assert.Null(account);
        }
    }
}