using Domain.Models;

namespace Domain.Handlers
{
    public class WithdrawHandler : IRequestHandler<WithdrawCommand, Account>
    {
        private readonly IAccountPort _IAccountPort;

        public WithdrawHandler(IAccountPort IAccountPort)
        {
            _IAccountPort = IAccountPort;
        }
        //Retrouner transactionStatus
        public async Task<Account> Handle(WithdrawCommand request, CancellationToken cancellationToken)
        {
            return await _IAccountPort.WithdrawByIdAsync(request.Id, request.Amount);
        }
    }
}
