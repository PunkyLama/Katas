using Domain.Commands;
using Domain.Exceptions;
using Domain.Injection;

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
            Statement transaction = new Statement();
            if (account == null || account.Id != request.Id)
            {
                throw new AccountNotFound(request.Id);
            }
            if (request.Amount <= 0)
            {
                transaction = transaction.CreateRejectedTransaction(Operation.Withdraw, request.Amount, account.Balance);
                await _persistencePort.Save(account, transaction);
                throw new AmountMustBeGreaterThanZero(request.Amount);
            }
            else if ((account.Balance - request.Amount) < 0)
            {
                transaction = transaction.CreateRejectedTransaction(Operation.Withdraw, request.Amount, account.Balance);
                await _persistencePort.Save(account, transaction);
                throw new InsufficientFunds(request.Amount, account.Balance);
            }
            else
            {
                var oldBalance = account.Balance;
                account.Balance -= request.Amount;
                transaction = transaction.CreateApprouvedTransaction(Operation.Withdraw, request.Amount, oldBalance, account.Balance);
            }
            await _persistencePort.Save(account, transaction);
            return account;
        }
    }
}
