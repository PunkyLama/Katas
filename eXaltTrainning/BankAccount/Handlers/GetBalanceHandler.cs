using KataBankAccount.Models;
using KataBankAccount.Queries;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace KataBankAccount.Handlers
{
    public class GetBalanceHandler : IRequestHandler<GetBalanceQuery, float>
    {
        private readonly IBankAccountRepository _bankAccountRepository;

        public GetBalanceHandler(IBankAccountRepository bankAccountRepository)
        {
            _bankAccountRepository = bankAccountRepository;
        }

        public async Task<float> Handle(GetBalanceQuery query, CancellationToken cancellationToken)
        {
            var bankAccountDetails = await _bankAccountRepository.GetBankAccountByIdAsync(query.Id);
            if (bankAccountDetails == null)
            {
                return default;
            }

            return await _bankAccountRepository.GetBalanceAsync(bankAccountDetails.Id);
        }
    }
}
