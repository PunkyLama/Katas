﻿using Domain.Commands;
using Domain.Exceptions;
using Domain.Handlers;
using Domain.Models;
using Domain.Ports.Driven;
using Domain.Queries;
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

        [Fact]
        public async Task DepositByIdAsync_WithInvalidAccount_ShouldThrow()
        {
            // Arrange
            var account = InitializeAccount();
            var invalidAccountId = 0;
            var amountToDeposit = 50;

            var persistencePortMock = new Mock<IAccountPersistencePort>();
            persistencePortMock.Setup(p => p.GetAccountByIdAsync(invalidAccountId)).ReturnsAsync(account);

            var handler = new DepositeHandler(persistencePortMock.Object);
            var request = new DepositCommand { Id = invalidAccountId, Amount = amountToDeposit };

            // Act
            var exception = await Assert.ThrowsAsync<AccountNotFound>(async () => await handler.HandleAsync(request));

            // Assert
            Assert.Equal($"Account with id {invalidAccountId} not found.", exception.Message);

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
            var exception = await Assert.ThrowsAsync<AmountMustBeGreaterThanZero>(async () => await handler.HandleAsync(request));

            // Assert
            Assert.Equal($"Amount {amountToDeposit} must be greater than zero.", exception.Message);
        }

        [Fact]
        public async Task WithdrawByIdAsync_WithInvalidAccount_ShouldThrow()
        {
            // Arrange
            var account = InitializeAccount();
            var invalidAccountId = 0;
            var amountToDeposit = 50;

            var persistencePortMock = new Mock<IAccountPersistencePort>();
            persistencePortMock.Setup(p => p.GetAccountByIdAsync(invalidAccountId)).ReturnsAsync(account);

            var handler = new WithdrawHandler(persistencePortMock.Object);
            var request = new WithdrawCommand { Id = invalidAccountId, Amount = amountToDeposit };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<AccountNotFound>(async () => await handler.HandleAsync(request));

            // Assert
            Assert.Equal($"Account with id {invalidAccountId} not found.", exception.Message);

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
            var exception = await Assert.ThrowsAsync<InsufficientFunds>(async() => await handler.HandleAsync(request));

            // Assert
            Assert.Equal($"Insufficient funds, balance : {account.Balance}, amount : {amountToWithdraw}.", exception.Message);
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
            var request = new BalanceQuery { Id = accountId };

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
            var invalidAccountId = 0;

            var persistencePortMock = new Mock<IAccountPersistencePort>();
            persistencePortMock.Setup(p => p.GetAccountByIdAsync(invalidAccountId)).ReturnsAsync(account);

            var handler = new GetBalanceHandler(persistencePortMock.Object);
            var request = new BalanceQuery { Id = invalidAccountId };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<AccountNotFound>(async () => await handler.HandleAsync(request));

            // Assert
            Assert.Equal($"Account with id {invalidAccountId} not found.", exception.Message);
        }
    }
}
