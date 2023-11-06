using Domain.Commands;
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
            var account = await _persistencePort.GetAccountByIdAsync(request.Id);
            Statement transaction;
            if (account == null || account.Id != request.Id)
            {
                throw new Exception("Account not found");
            }
            if (request.Amount <= 0)
            {
                transaction = new Statement(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), Operation.Deposit, StatementStatus.Rejected, request.Amount, account.Balance);
                await _persistencePort.Save(account, transaction);
                throw new Exception("Amount must be greater than 0");
            }
            else
            {
                var oldBalance = account.Balance;
                account.Balance += request.Amount;
                transaction = new Statement(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), Operation.Deposit, StatementStatus.Approuved, request.Amount, oldBalance, account.Balance);
            }
            await _persistencePort.Save(account, transaction);
            return account;
        }
    }
}
