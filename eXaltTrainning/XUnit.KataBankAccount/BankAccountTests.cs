using KataBankAccount.Models;
using KataBankAccount.Repository;
using KataBankAccount.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace XUnit.KataBankAccount
{
    [TestCaseOrderer("XUnit.KataBankAccount.PriorityOrderer", "XUnit.KataBankAccount")]
    public class BankAccountTests
    {
        private BankAccountRepository _service { get; set; }
        public BankAccountTests()
        {
            var options = new DbContextOptionsBuilder<DbContextClass>()
            //.UseSqlServer("Data Source=PUNKY-PC; Database=BankAccount ;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False")
            .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
            .Options;
            var context = new DbContextClass(options);
            _service = new BankAccountRepository(context);
        }

        [Fact, Order(1)]        
        public void AddBankAccountAsync_ShouldReturnBankAccount()
        {
            Debug.WriteLine("AddBankAccountAsync_ShouldReturnBankAccount");
            //Arrange
            var bankAccountToAdd = new AddBankAccount()
            {
                Name = "Guillaume",
                Balance = 1000.4f,
            };

            //Act
            var result = _service.AddBankAccountAsync(bankAccountToAdd);

            //Assert
            Assert.NotNull(result);
        }

        [Fact, Order(2)]
        public void GetBankAccountByIdAsync_ShouldReturnBankAccount()
        {
            Debug.WriteLine("GetBankAccountByIdAsync_ShouldReturnBankAccount");
            // Arrange
            int accountId = 1;

            // Act
            var result = _service.GetBankAccountByIdAsync(accountId);

            // Assert
            Assert.NotNull(result);
        }

        [Fact, Order(3)]
        public void GetBalanceAsync_ShouldReturnFloat()
        {
            Debug.WriteLine("GetBalanceAsync_ShouldReturnFloat");
            //Arrange
            int accountId = 1;

            //Act
            var result = _service.GetBalanceAsync(accountId);

            //Assert
            Assert.Equal(1000.4f, result.Result);
        }       

        [Fact, Order(4)]
        public void GetHistoryAsync_ShouldReturnString()
        {
            Debug.WriteLine("GetHistoryAsync_ShouldReturnString");
            //Arrange
            int accountId = 1;

            //Act
            var result = _service.GetHistoryAsync(accountId);

            //Assert
            Assert.NotNull(result);
        }

        [Theory, Order(5)]
        [InlineData(140.6)]
        [InlineData(1000.6)]
        [InlineData(65)]
        public void DepositAsync_ShouldReturnBankAccount(float amoutToAdd)
        {
            Debug.WriteLine("DepositAsync_ShouldReturnBankAccount");
            //Arrange
            int accountId = 1;

            //Act
            var result = _service.DepositAsync(accountId, amoutToAdd);

            //Assert
            Assert.NotNull(result);            
        }

        [Theory, Order(6)]
        [InlineData(140.6)]
        [InlineData(10000000)]
        [InlineData(65)]
        public void WithdrawAsync_ShouldReturnBankAccount(float amoutToSubtract)
        {
            Debug.WriteLine("WithdrawAsync_ShouldReturnBankAccount");
            //Arrange
            int accountId = 1;

            //Act
            var result = _service.WithdrawAsync(accountId, amoutToSubtract);

            //Assert
            Assert.NotNull(result);
            Assert.DoesNotContain("-", result.Result.Balance.ToString());
            
        }
    }
}