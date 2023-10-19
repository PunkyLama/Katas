namespace Domain.Ports.Driving
{
    public interface IAccountPort
    {
        public Task<Account> DepositByIdAsync(int id, float amount);
        public Task<Account> WithdrawByIdAsync(int id, float amount);
        public Task<ICollection<TransactionHistory>> GetStatementByIdAsync (int id);
    }
}
