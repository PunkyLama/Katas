namespace Domain.Handlers
{
    public class GetHistoryHandler : IRequestHandler<GetHistoryQuery, ICollection<TransactionHistory>>
    {
        private readonly IAccountPersistencePort _persistencePort;

        public GetHistoryHandler(IAccountPersistencePort persistencePort)
        {
            _persistencePort = persistencePort;
        }
        public async Task<ICollection<TransactionHistory>> Handle(GetHistoryQuery request, CancellationToken cancellationToken)
        {
            var account = await _persistencePort.GetAccountByIdAsync(request.Id);
            if (account == null)
            {
                return default;
            }
            return account.TransactionHistories;
        }
    }
}
