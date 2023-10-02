using KataBankAccount.Models;
using KataBankAccount.Queries;
using MediatR;

namespace KataBankAccount.Handlers
{
    public class GetBankAccountByIdHandler : IRequestHandler<GetBankAccountByIdQuery, BankAccount>
    {
        private readonly IBankAccountRepository _bankAccountRepository;

        public GetBankAccountByIdHandler(IBankAccountRepository bankAccountRepository)
        {
            _bankAccountRepository = bankAccountRepository;
        }

        public async Task<BankAccount> Handle(GetBankAccountByIdQuery query, CancellationToken cancellationToken)
        {
            return await _bankAccountRepository.GetBankAccountByIdAsync(query.Id);
        }
    }
}
