using Domain.Commands;
using Domain.Injection;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Handlers
{
    public class WithdrawHandler : IHandler<WithdrawCommand, Account>
    {
        private readonly IAccountPersistencePort _persistencePort;

        public WithdrawHandler(IAccountPersistencePort persistencePort)
        {
            _persistencePort = persistencePort;
        }

        public async Task<Account> HandleAsync(WithdrawCommand request)
        {
            var account = await _persistencePort.GetAccountByIdAsync(request.Id);
            Statement transaction;
            if (account == null)
            {
                throw new Exception("Account not found");
            }
            if (request.Amount <= 0)
            {
                transaction = new Statement(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), Operation.Withdraw, StatementStatus.Rejected, request.Amount, account.Balance);
                await _persistencePort.Save(account, transaction);
                throw new Exception("Amount must be greater than 0");
            }
            else if ((account.Balance - request.Amount) < 0)
            {
                transaction = new Statement(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), Operation.Withdraw, StatementStatus.Rejected, request.Amount, account.Balance);
                await _persistencePort.Save(account, transaction);
                throw new Exception("Insufficient funds in the account");
            }
            else
            {
                var oldBalance = account.Balance;
                account.Balance -= request.Amount;
                transaction = new Statement(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), Operation.Withdraw, StatementStatus.Approuved, request.Amount, oldBalance, account.Balance);
            }
            await _persistencePort.Save(account, transaction);
            return account;
        }
    }
}
