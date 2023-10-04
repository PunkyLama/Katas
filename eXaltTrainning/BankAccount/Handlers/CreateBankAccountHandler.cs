using KataBankAccount.Commands;
using KataBankAccount.Models;
using MediatR;

namespace KataBankAccount.Handlers
{
    public class CreateBankAccountHandler : IRequestHandler<CreateBankAccountCommand, BankAccount>
    {
        private readonly IBankAccountRepository _bankAccountRepository;

        public CreateBankAccountHandler(IBankAccountRepository bankAccountRepository)
        {
            _bankAccountRepository = bankAccountRepository;
        }

        public async Task<BankAccount> Handle(CreateBankAccountCommand command, CancellationToken cancellationToken)
        {
            var bankAccountDetails = new AddBankAccount()
            {
                Name = command.Name,
                Balance = command.Balance,
            };

            return await _bankAccountRepository.AddBankAccountAsync(bankAccountDetails);
        }
    }
}
