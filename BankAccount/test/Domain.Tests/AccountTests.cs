namespace Domain.Tests
{
    [TestCaseOrderer("Domain.Tests.PriorityOrderer", "Domain.Tests")]
    public class AccountTests
    {

        private readonly Mock<Account> _account;
        private readonly Mock<IAccountPersistencePort> _persistencePort;

        /*
        private static GlobalInMemory inMemory = new GlobalInMemory();
        private DbContextBank _dbContext = new DbContextBank(inMemory._options);

        [Theory, Order(1)]
        [InlineData(100, 200)]
        [InlineData(94.5, 294.5)]
        public async Task DepositByIdAsync_ShouldDepositAmountToAccount(float amount, float expected)
        {   
            // Arrange
            var account = new AccountAdapter(_dbContext);
            // Act
            var result = await account.DepositByIdAsync(1, amount);
            // Assert
            Assert.NotNull(result);
            Assert.IsType<Account>(result);
            Assert.Equal(expected, result.Balance);            
        }

        [Theory, Order(2)]
        [InlineData(94.5, 200)]        
        [InlineData(1000, 200)]
        public async Task WithdrawByIdAsync_ShouldWithdrawAmountToAccount(float amount, float expected)
        {
            // Arrange
            var account = new AccountAdapter(_dbContext);
            // Act
            var result = await account.WithdrawByIdAsync(1, amount);
            // Assert
            Assert.NotNull(result);
            Assert.IsType<Account>(result);
            Assert.Equal(expected, result.Balance);
        }

        [Fact , Order(3)]
        public async Task GetTransactionHistories_ShouldReturnTransactionList()
        {
            // Arrange
            var account = new AccountAdapter(_dbContext);
            // Act
            var result = await account.GetStatementByIdAsync(1);
            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<TransactionHistory>>(result);
            Assert.Equal(4, result.Count());
        }

        [Fact , Order(4)]
        public async Task GetAccountById_ShouldReturnAccount()
        {
            // Arrange
            var account = new AccountAdapter(_dbContext);
            // Act
            var result = await account.GetAccountByIdAsync(1);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<Account>(result);
        }
        */
    }
}
