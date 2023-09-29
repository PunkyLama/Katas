namespace KataBankAccount.Models
{
    public interface IBankAccount
    {
        public void Deposit(int id, float amountToAdd);
        public void Withdraw(int id, float amountToSubtract);
        public float Balance(int id);
        public string History(int id);
    }
}
