using KataBankAccount.Models;
using KataBankAccount.Queries;
using MediatR;

namespace KataBankAccount.Handlers
{
    public class GetHistoryHandler : IRequestHandler<GetHistoryQuery, string>
    {
        private readonly IBankAccountRepository _bankAccountRepository;

        public GetHistoryHandler(IBankAccountRepository bankAccountRepository)
        {
            _bankAccountRepository = bankAccountRepository;
        }

        public async Task<string> Handle(GetHistoryQuery query, CancellationToken cancellationToken)
        {
            var bankAccountDetails = await _bankAccountRepository.GetBankAccountByIdAsync(query.Id);
            if (bankAccountDetails == null)
            {
                return default;
            }

            return await _bankAccountRepository.GetHistoryAsync(bankAccountDetails.Id);
        }
    }
}
