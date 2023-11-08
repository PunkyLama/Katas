using Domain.Injection;
using Domain.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                throw new Exception("Account not found");
            }
            return account.Balance;
        }
    }
}
