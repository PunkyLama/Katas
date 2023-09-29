namespace BankAccountCore
{

    public interface IAccountService
    {
        void Deposit(Guid accountId, decimal amount);
        void Withdraw(Guid accountId, decimal amount);
        void Transfer(Guid fromAccountId, Guid toAccountId, decimal amount);
    }

    public interface IAccountRepository
    {
        Account Get(Guid accountId);
        void Update(Account account);
    }

    public class Account
    {
        public Guid Id { get; set; }
        public decimal Balance { get; private set; }

        public void Deposit(decimal amount)
        {
            //Deposit logic
            Balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            //Withdraw logic
            if(amount > Balance)
            {
                throw new InvalidOperationException("Insufficient balance");
            }
            Balance -= amount;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {

        }
    }
}