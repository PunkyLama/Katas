using KataBankAccount.Commands;
using KataBankAccount.Models;
using MediatR;

namespace KataBankAccount.Handlers
{
    public class WithdrawHandler : IRequestHandler<WithdrawCommand, int>
    {
        private readonly IBankAccountRepository _bankAccountRepository;

        public WithdrawHandler(IBankAccountRepository bankAccountRepository)
        {
            _bankAccountRepository = bankAccountRepository;
        }

        public async Task<int> Handle(WithdrawCommand command, CancellationToken cancellationToken)
        {
            var bankAccountDetails = await _bankAccountRepository.GetBankAccountByIdAsync(command.Id);
            if (bankAccountDetails == null)
            {
                return default;
            }

            return await _bankAccountRepository.WithdrawAsync(bankAccountDetails.Id, command.amountToSubstract);
        }
    }
}
