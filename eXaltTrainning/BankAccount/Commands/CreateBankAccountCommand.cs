using KataBankAccount.Models;
using MediatR;

namespace KataBankAccount.Commands
{
    public class CreateBankAccountCommand : IRequest<BankAccount>
    {
        public string Name { get; set; }
        public float Balance { get; set; }

        public CreateBankAccountCommand(string name, float balance) 
        {
            this.Name = name;
            this.Balance = balance;
        }
    }
}
