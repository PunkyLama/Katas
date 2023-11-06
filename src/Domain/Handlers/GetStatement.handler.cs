using Domain.Commands;
using Domain.Injection;
using Domain.Models;

namespace Domain.Handlers
{
    public class GetStatementHandler : IHandler<StatementCommand, ICollection<Statement>>
    {
        private readonly IAccountPersistencePort _persistencePort;

        public GetStatementHandler(IAccountPersistencePort persistencePort)
        {
            _persistencePort = persistencePort;
        }

        public async Task<ICollection<Statement>> HandleAsync(StatementCommand request)
        {
            var transactions = await _persistencePort.GetStatementsByAccountIdAsync(request.Id, request.Element);
            if (transactions == null)
            {
                throw new Exception("Account not found");
            }          
            return transactions;
        }
    }
}
