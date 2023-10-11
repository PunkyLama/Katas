using Domain.Models;

namespace Domain.Handlers
{
    public class WithdrawHandler : IRequestHandler<WithdrawCommand, Account>
    {
        private readonly IAccountPersistencePort _persistencePort;

        public WithdrawHandler(IAccountPersistencePort persistencePort)
        {
            _persistencePort = persistencePort;
        }
        //Retrouner transactionStatus
        public async Task<Account> Handle(WithdrawCommand request, CancellationToken cancellationToken)
        {
            var account = await _persistencePort.GetAccountByIdAsync(request.Id);
            TransactionHistory transaction;
            if ((account.Balance - request.Amount) < 0)
            {
                transaction = new TransactionHistory(DateTime.Now, Operation.Withdraw, TransactionStatus.Rejected);
            }
            else
            {
                account.Balance -= request.Amount;
                transaction = new TransactionHistory(DateTime.Now, Operation.Withdraw, TransactionStatus.Approuved);
            }
            account.TransactionHistories.Add(transaction);
            await _persistencePort.SaveAccount();
            return account;
        }
    }
}
