using Domain.Injection;

namespace Domain.Commands
{
    public class Deposit : IRequest<Account>
    {
        public int Id { get; set; }
        public float Amount { get; set; }

        public Deposit(int id, float amount)
        {
            Id = id;
            Amount = amount;
        }
    }
}
