using Domain.Injection;

namespace Domain.Commands
{
    public class DepositCommand : IRequest<Account>
    {
        public int Id { get; set; }
        public float Amount { get; set; }
    }
}
