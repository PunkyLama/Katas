namespace Domain.Handlers
{
    public class DepositHandler : IRequestHandler<DepositCommand, Account>
    {
        private readonly IAccountPort _IAccountPort;

        public DepositHandler(IAccountPort IAccountPort)
        {
            _IAccountPort = IAccountPort;
        }

        //Test si montant depot negatif
        public async Task<Account> Handle(DepositCommand request, CancellationToken cancellationToken)
        {
            return await _IAccountPort.DepositByIdAsync(request.Id, request.Amount);
        }
    }
}
