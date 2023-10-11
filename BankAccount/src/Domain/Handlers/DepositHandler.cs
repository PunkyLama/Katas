using Domain.Models;

namespace Domain.Handlers
{
    public class DepositHandler : IRequestHandler<DepositCommand, Account>
    {
        private readonly IAccountPersistencePort _persistencePort;
        
        public DepositHandler(IAccountPersistencePort persistencePort)
        {
            _persistencePort = persistencePort;
        }

        //Test si montant depot negatif
        public async Task<Account> Handle(DepositCommand request, CancellationToken cancellationToken)
        {
            var account = await _persistencePort.GetAccountByIdAsync(request.Id);
            account.Balance += request.Amount;
            TransactionHistory transaction = new TransactionHistory(DateTime.Now, Operation.Deposit, TransactionStatus.Approuved);
            account.TransactionHistories.Add(transaction);
            await _persistencePort.SaveAccount();
            return account;
        }
    }
}
