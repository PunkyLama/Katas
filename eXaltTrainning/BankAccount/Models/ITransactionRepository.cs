namespace KataBankAccount.Models
{
    public interface ITransactionRepository
    {
        public Transaction AddTransaction(int id, Task<BankAccount>? entity = default, float amount = default, float balance = default);
    }
}
