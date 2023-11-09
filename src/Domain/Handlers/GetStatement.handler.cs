using Domain.Exceptions;
using Domain.Injection;
using Domain.Queries;

namespace Domain.Handlers
{
    public class GetStatementHandler : IHandler<StatementQuery, ICollection<Statement>>
    {
        private readonly IAccountPersistencePort _persistencePort;

        public GetStatementHandler(IAccountPersistencePort persistencePort)
        {
            _persistencePort = persistencePort;
        }

        public async Task<ICollection<Statement>> HandleAsync(StatementQuery request)
        {
            var transactions = await _persistencePort.GetStatementsByAccountIdAsync(request.Id, request.Element);
            if (transactions == null)
            {
                throw new AccountNotFound(request.Id);
            }          
            return transactions;
        }
    }
}
