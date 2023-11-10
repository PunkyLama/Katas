using Domain.Exceptions;
using Domain.Injection;
using Domain.Queries;

namespace Domain.Handlers
{
    public class GetBalanceHandler : IHandler<BalanceQuery, float>
    {
        private readonly IAccountPersistencePort _persistencePort;

        public GetBalanceHandler(IAccountPersistencePort persistencePort)
        {
            _persistencePort = persistencePort;
        }

        public async Task<float> HandleAsync(BalanceQuery request)
        {
            var account = await _persistencePort.GetAccountByIdAsync(request.Id);
            if (account == null || account.Id != request.Id)
            {
                throw new AccountNotFound(request.Id);
            }
            return account.Balance;
        }
    }
}
