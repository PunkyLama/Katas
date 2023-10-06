using KataBankAccount.Commands;
using KataBankAccount.Models;
using MediatR;

namespace KataBankAccount.Handlers
{
    //Domaine
    public class DepositeHandler : IRequestHandler<DepositeCommand, BankAccount>
    {
        private readonly IBankAccountRepository _bankAccountRepository;

        public DepositeHandler(IBankAccountRepository bankAccountRepository)
        {
            _bankAccountRepository = bankAccountRepository;
        }

        public async Task<BankAccount> Handle(DepositeCommand command, CancellationToken cancellationToken)
        {
            var bankAccountDetails = await _bankAccountRepository.GetBankAccountByIdAsync(command.Id);
            if (bankAccountDetails == null)
            {
                return default;
            }

            return await _bankAccountRepository.DepositAsync(bankAccountDetails.BankAccountId, command.amountToAdd);
        }
    }
}
