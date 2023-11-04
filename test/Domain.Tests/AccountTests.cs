using Domain.Commands;
using Domain.Handlers;
using Domain.Models;
using Domain.Ports.Driven;
using Moq;
using Xunit;

namespace Domain.Tests
{
    [TestCaseOrderer("Domain.Tests.PriorityOrderer", "Domain.Tests")]
    public class AccountTests
    {
        public static Account InitializeAccountWithTransactions()
        {
            var account = new Account
            {
                Id = 1,
                Balance = 100, // Solde initial
            };

            return account;
        }

        [Fact]
        public async Task DepositByIdAsync_WithPositiveAmount_ShouldIncreaseBalanceAndAddTransaction()
        {
            // Arrange
            var account = InitializeAccountWithTransactions();
            var accountId = account.Id;
            var amountToDeposit = 50;

            var persistencePortMock = new Mock<IAccountPersistencePort>();
            persistencePortMock.Setup(p => p.GetAccountByIdAsync(accountId)).ReturnsAsync(account);

            var handler = new DepositeHandler(persistencePortMock.Object);
            var request = new DepositCommand { Id = accountId, Amount = amountToDeposit };

            // Act
            var result = await handler.HandleAsync(request);

            // Assert
            Assert.Equal(150, result.Balance); // Le solde attendu après le dépôt

        }

        [Fact]
        public async Task DepositByIdAsync_WithNegativeAmount_ShouldNotChangeBalanceAndAddRejectedTransaction()
        {
            // Arrange
            var account = InitializeAccountWithTransactions();
            var accountId = account.Id;
            var amountToDeposit = -50;

            var persistencePortMock = new Mock<IAccountPersistencePort>();
            persistencePortMock.Setup(p => p.GetAccountByIdAsync(accountId)).ReturnsAsync(account);

            var handler = new DepositeHandler(persistencePortMock.Object);
            var request = new DepositCommand { Id = accountId, Amount = amountToDeposit };

            // Act
            var result = await handler.HandleAsync(request);

            // Assert
            Assert.Fail("Amount must be greater than 0");
        }

        [Fact]
        public async Task WithdrawByIdAsync_WithValidAmount_ShouldDecreaseBalanceAndAddTransaction()
        {
            // Arrange
            var account = InitializeAccountWithTransactions();
            var accountId = account.Id;
            var amountToWithdraw = 50;

            var persistencePortMock = new Mock<IAccountPersistencePort>();
            persistencePortMock.Setup(p => p.GetAccountByIdAsync(accountId)).ReturnsAsync(account);

            var handler = new WithdrawHandler(persistencePortMock.Object);
            var request = new WithdrawCommand { Id = accountId, Amount = amountToWithdraw };

            // Act
            var result = await handler.HandleAsync(request);

            // Assert
            Assert.Equal(50, result.Balance);
        }

        [Fact]
        public async Task WithdrawByIdAsync_WithInvalidAmount_ShouldNotChangeBalanceAndAddRejectedTransaction()
        {
            // Arrange
            var account = InitializeAccountWithTransactions();
            var accountId = account.Id;
            var amountToWithdraw = 200;

            var persistencePortMock = new Mock<IAccountPersistencePort>();
            persistencePortMock.Setup(p => p.GetAccountByIdAsync(accountId)).ReturnsAsync(account);

            var handler = new WithdrawHandler(persistencePortMock.Object);
            var request = new WithdrawCommand { Id = accountId, Amount = amountToWithdraw };

            // Act
            var result = await handler.HandleAsync(request);
            string aa = "aa";

            // Assert
            Assert.Fail("Insufficient funds in the account");
        }
    }
}
