using Domain.Adapters;

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
                Balance = 500, // Initial balance
                TransactionHistories = new List<TransactionHistory>()
            };

            var persistencePortMock = new Mock<IAccountPersistencePort>();
            persistencePortMock.Setup(p => p.GetAccountByIdAsync(1)).ReturnsAsync(account);

            return persistencePortMock.Object;
        }

        [Fact]
        public async Task DepositByIdAsync_ShouldIncreaseBalanceAndAddTransaction()
        {
            // Arrange

            var accountId = 1;
            var amountToDeposit = 100;

            var adapter = new DomainAccoutAdapter(InitializeData());

            // Act
            var result = await adapter.DepositByIdAsync(accountId, amountToDeposit);
            var TransactionList = result.TransactionHistories.ToList();

            // Assert
            Assert.Equal(accountId, result.Id);
            Assert.Equal(600, result.Balance); // Expected balance after deposit
            Assert.Single(result.TransactionHistories); // One transaction added
            Assert.Equal(Operation.Deposit, TransactionList[0].Operation);
            Assert.Equal(TransactionStatus.Approuved, TransactionList[0].TransactionStatus);
        }

        [Fact]
        public async Task WithdrawByIdAsync_ShouldDecreaseBalanceAndAddTransaction()
        {
            // Arrange
            var accountId = 1;
            var amountToWithdraw = 50;

            var adapter = new DomainAccoutAdapter(InitializeData());

            // Act
            var result = await adapter.WithdrawByIdAsync(accountId, amountToWithdraw);
            var TransactionList = result.TransactionHistories.ToList();

            // Assert
            Assert.Equal(accountId, result.Id);
            Assert.Equal(450, result.Balance); // Expected balance after withdrawal
            Assert.Single(result.TransactionHistories); // One transaction added
            Assert.Equal(Operation.Withdraw, TransactionList[0].Operation);
            Assert.Equal(TransactionStatus.Approuved, TransactionList[0].TransactionStatus);
        }
    }
}
