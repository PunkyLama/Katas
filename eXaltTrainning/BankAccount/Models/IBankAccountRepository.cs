namespace KataBankAccount.Models
{
    public interface IBankAccountRepository
    {
        public Task<BankAccount> AddBankAccountAsync(AddBankAccount AddbankAccount);
        public Task<BankAccount> GetBankAccountByIdAsync(int id);
        public Task<BankAccount> DepositAsync(int id, float amountToAdd);
        public Task<BankAccount> WithdrawAsync(int id, float amountToSubtract);
        public Task<float> GetBalanceAsync(int id);
        public Task<string> GetHistoryAsync(int id);
    }
}
