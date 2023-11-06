﻿using Domain.Commands;
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
        public static Account InitializeAccount()
        {
            var account = new Account
            {
                Id = 1,
                Balance = 100, // Solde initial
            };

            return account;
        }
        public static (Account, List<Statement>) InitializeAccountWithTransaction()
        {
            var account = new Account
            {
                Id = 1,
                Balance = 100, // Solde initial
            };
            var statement = new List<Statement>{new Statement(
                DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                Operation.Deposit,
                StatementStatus.Approuved,
                50,
                100,
                150 
            )};

            return (account, statement);
        }

        [Fact]
        public async Task DepositByIdAsync_WithInvalidAccount_ShouldThrow()
        {
            // Arrange
            var account = InitializeAccount();
            var accountId = 0;
            var amountToDeposit = 50;

            var persistencePortMock = new Mock<IAccountPersistencePort>();
            persistencePortMock.Setup(p => p.GetAccountByIdAsync(accountId)).ReturnsAsync(account);

            var handler = new DepositeHandler(persistencePortMock.Object);
            var request = new DepositCommand { Id = accountId, Amount = amountToDeposit };

            // Act
            var result = Assert.ThrowsAsync<Exception>(async () => await handler.HandleAsync(request));

            // Assert
            Assert.Equal("Account not found", result.Result.Message); // Le solde attendu après le dépôt

        }

        [Fact]
        public async Task DepositByIdAsync_WithPositiveAmount_ShouldIncreaseBalanceAndAddTransaction()
        {
            // Arrange
            var account = InitializeAccount();
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
            var account = InitializeAccount();
            var accountId = account.Id;
            var amountToDeposit = -50;

            var persistencePortMock = new Mock<IAccountPersistencePort>();
            persistencePortMock.Setup(p => p.GetAccountByIdAsync(accountId)).ReturnsAsync(account);

            var handler = new DepositeHandler(persistencePortMock.Object);
            var request = new DepositCommand { Id = accountId, Amount = amountToDeposit };

            // Act & Assert
            var result = Assert.ThrowsAsync<Exception>(async () => await handler.HandleAsync(request));

            // Assert
            Assert.Equal("Amount must be greater than 0", result.Result.Message);
        }

        [Fact]
        public async Task WithdrawByIdAsync_WithInvalidAccount_ShouldThrow()
        {
            // Arrange
            var account = InitializeAccount();
            var accountId = 0;
            var amountToDeposit = 50;

            var persistencePortMock = new Mock<IAccountPersistencePort>();
            persistencePortMock.Setup(p => p.GetAccountByIdAsync(accountId)).ReturnsAsync(account);

            var handler = new WithdrawHandler(persistencePortMock.Object);
            var request = new WithdrawCommand { Id = accountId, Amount = amountToDeposit };

            // Act & Assert
            var result = Assert.ThrowsAsync<Exception>(async () => await handler.HandleAsync(request));

            // Assert
            Assert.Equal("Account not found", result.Result.Message);

        }

        [Fact]
        public async Task WithdrawByIdAsync_WithValidAmount_ShouldDecreaseBalanceAndAddTransaction()
        {
            // Arrange
            var account = InitializeAccount();
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
            var account = InitializeAccount();
            var accountId = account.Id;
            var amountToWithdraw = 200;

            var persistencePortMock = new Mock<IAccountPersistencePort>();
            persistencePortMock.Setup(p => p.GetAccountByIdAsync(accountId)).ReturnsAsync(account);

            var handler = new WithdrawHandler(persistencePortMock.Object);
            var request = new WithdrawCommand { Id = accountId, Amount = amountToWithdraw };

            // Act & Assert
            var result = Assert.ThrowsAsync<Exception>(async() => await handler.HandleAsync(request));

            // Assert
            Assert.Equal("Insufficient funds in the account", result.Result.Message);
        }

        [Fact]
        public async Task GetBalanceByIdAsync_WithValidId_ShouldReturnBalance()
        {
            // Arrange
            var account = InitializeAccount();
            var accountId = account.Id;

            var persistencePortMock = new Mock<IAccountPersistencePort>();
            persistencePortMock.Setup(p => p.GetAccountByIdAsync(accountId)).ReturnsAsync(account);

            var handler = new GetBalanceHandler(persistencePortMock.Object);
            var request = new BalanceCommand { Id = accountId };

            // Act
            var result = await handler.HandleAsync(request);

            // Assert
            Assert.Equal(100, result);
        }

        [Fact]
        public async Task GetBalanceByIdAsync_WithInvalidId_ShouldThrow()
        {
            // Arrange
            var account = InitializeAccount();
            var accountId = 0;

            var persistencePortMock = new Mock<IAccountPersistencePort>();
            persistencePortMock.Setup(p => p.GetAccountByIdAsync(accountId)).ReturnsAsync(account);

            var handler = new GetBalanceHandler(persistencePortMock.Object);
            var request = new BalanceCommand { Id = accountId };

            // Act & Assert
            var result = Assert.ThrowsAsync<Exception>(async () => await handler.HandleAsync(request));

            // Assert
            Assert.Equal("Account not found", result.Result.Message);
        }
        
        [Fact]
        public async Task GetStatementsListByAccountIdAsync_WithValidId_ShouldReturnListOfStatement()
        {
            // Arrange
            var initialize = InitializeAccountWithTransaction();
            var account = initialize.Item1;
            var statements = initialize.Item2;
            var elements = 10;

            var persistencePortMock = new Mock<IAccountPersistencePort>();
            persistencePortMock.Setup(p => p.GetStatementsByAccountIdAsync(account.Id, elements)).ReturnsAsync(statements);

            var handler = new GetStatementHandler(persistencePortMock.Object);
            var request = new StatementCommand { Id = account.Id, Element = elements };

            // Act
            var result = await handler.HandleAsync(request);

            // Assert
            Assert.Equal(1, result.Count());
        }
    }
}
