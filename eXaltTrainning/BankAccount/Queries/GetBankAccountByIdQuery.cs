using KataBankAccount.Models;
using MediatR;

namespace KataBankAccount.Queries
{
    public class GetBankAccountByIdQuery : IRequest<BankAccount>
    {
        public int Id { get; set; }
    }
}
