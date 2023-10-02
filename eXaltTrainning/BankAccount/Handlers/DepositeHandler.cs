using KataBankAccount.Commands;
using KataBankAccount.Models;
using MediatR;

namespace KataBankAccount.Handlers
{
    public class DepositeHandler : IRequestHandler<DepositeCommand, int>
    {
        private readonly IBankAccountRepository _bankAccountRepository;

        public DepositeHandler(IBankAccountRepository bankAccountRepository)
        {
            _bankAccountRepository = bankAccountRepository;
        }

        public async Task<int> Handle(DepositeCommand command, CancellationToken cancellationToken)
        {
            var bankAccountDetails = await _bankAccountRepository.GetBankAccountByIdAsync(command.Id);
            if (bankAccountDetails == null)
            {
                return default;
            }

            return await _bankAccountRepository.DepositAsync(bankAccountDetails.Id, command.amountToAdd);
        }
    }
}
