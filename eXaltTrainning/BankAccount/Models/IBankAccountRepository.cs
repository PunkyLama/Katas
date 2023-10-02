namespace KataBankAccount.Models
{
    public interface IBankAccountRepository
    {
        public Task<BankAccount> GetBankAccountByIdAsync(int id);
        public Task<int> DepositAsync(int id, float amountToAdd);
        public Task<int> WithdrawAsync(int id, float amountToSubtract);
        public Task<float> GetBalanceAsync(int id);
        public Task<string> GetHistoryAsync(int id);
    }
}
