using Domain.Commands;
using Domain.Exceptions;
using Domain.Injection;

namespace Domain.Handlers
{
    public class DepositeHandler : IHandler<DepositCommand, Account>
    {
        private readonly IAccountPersistencePort _persistencePort;

        public DepositeHandler(IAccountPersistencePort persistencePort)
        {
            _persistencePort = persistencePort;
        }

        public async Task<Account> HandleAsync(DepositCommand request)
        {
            Account account = await _persistencePort.GetAccountByIdAsync(request.Id);
            Statement transaction = new Statement();
            if (account == null || account.Id != request.Id)
            {
                throw new AccountNotFound(account.Id);
            }
            if (request.Amount <= 0)
            {
                transaction = transaction.CreateRejectedTransaction(Operation.Deposit, request.Amount, account.Balance);
                await _persistencePort.Save(account, transaction);
                throw new AmountMustBeGreaterThanZero(request.Amount);
            }
            else
            {
                var oldBalance = account.Balance;
                account.Balance += request.Amount;
                transaction = transaction.CreateApprouvedTransaction(Operation.Deposit, request.Amount, oldBalance, account.Balance);
            }
            await _persistencePort.Save(account, transaction);
            return account;
        }
    }
}
