using Domain.Adapters;
using Domain.Models;
using Domain.Ports.Driven;
using Moq;
using Xunit;

namespace Domain.Tests
{
    [TestCaseOrderer("Domain.Tests.PriorityOrderer", "Domain.Tests")]
    public class AccountTests
    {
        public IAccountPersistencePort InitializeData()
        {
            var account = new Account
            {
                Id = 1,
                Balance = 100, // Initial balance
                TransactionHistories = new List<TransactionHistory>()
            };

            var persistencePortMock = new Mock<IAccountPersistencePort>();
            persistencePortMock.Setup(p => p.GetAccountByIdAsync(1)).ReturnsAsync(account);

            return persistencePortMock.Object;
        }

        [Fact]
        public async Task DepositByIdAsync_WithPositiveAmount_ShouldIncreaseBalanceAndAddTransaction()
        {
            // Arrange

            var accountId = 1;
            var amountToDeposit = 100;

            var adapter = new DomainAccoutAdapter(InitializeData());

            // Act
            var result = await adapter.DepositByIdAsync(accountId, amountToDeposit);

            // Assert
            Assert.Equal(200, result.Balance); // Expected balance after deposit
            Assert.Single(result.TransactionHistories); // One transaction added
            Assert.Equal(Operation.Deposit, result.TransactionHistories[0].Operation);
            Assert.Equal(TransactionStatus.Approuved, result.TransactionHistories[0].TransactionStatus);
        }

        [Fact]
        public async Task DepositByIdAsync_WithNegativeAmount_ShouldNotChangeBalanceAndAddRejectedTransaction()
        {
            // Arrange
            var accountId = 1;
            var amountToDeposit = -50;

            var adapter = new DomainAccoutAdapter(InitializeData());

            // Act
            var result = await adapter.DepositByIdAsync(accountId, amountToDeposit);

            // Assert
            Assert.Equal(100, result.Balance);
            Assert.Single(result.TransactionHistories);
            Assert.Equal(Operation.Deposit, result.TransactionHistories[0].Operation);
            Assert.Equal(TransactionStatus.Rejected, result.TransactionHistories[0].TransactionStatus);
        }

        [Fact]
        public async Task WithdrawByIdAsync_WithValidAmount_ShouldDecreaseBalanceAndAddTransaction()
        {
            // Arrange
            var accountId = 1;
            var amountToWithdraw = 50;

            var adapter = new DomainAccoutAdapter(InitializeData());

            // Act
            var result = await adapter.WithdrawByIdAsync(accountId, amountToWithdraw);

            // Assert
            Assert.Equal(50, result.Balance);
            Assert.Single(result.TransactionHistories);
            Assert.Equal(Operation.Withdraw, result.TransactionHistories[0].Operation);
            Assert.Equal(TransactionStatus.Approuved, result.TransactionHistories[0].TransactionStatus);
        }

        [Fact]
        public async Task WithdrawByIdAsync_WithInvalidAmount_ShouldNotChangeBalanceAndAddRejectedTransaction()
        {
            // Arrange
            var accountId = 1;
            var amountToWithdraw = 200;

            var adapter = new DomainAccoutAdapter(InitializeData());

            // Act
            var result = await adapter.WithdrawByIdAsync(accountId, amountToWithdraw);

            // Assert
            Assert.Equal(100, result.Balance);
            Assert.Single(result.TransactionHistories);
            Assert.Equal(Operation.Withdraw, result.TransactionHistories[0].Operation);
            Assert.Equal(TransactionStatus.Rejected, result.TransactionHistories[0].TransactionStatus);
        }
    }
}
