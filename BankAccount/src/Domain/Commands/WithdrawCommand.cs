using MediatR;

namespace Domain.Commands
{
    public class WithdrawCommand : IRequest<Account>
    {
        public int Id { get; set; }
        public float Amount { get; set; }

        public WithdrawCommand(int id, float amount)
        {
            Id = id;
            this.Amount = amount;
        }
    }
}
