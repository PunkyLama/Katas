﻿namespace Domain.Ports.Driving
{
    public interface IAccountPort
    {
        public Task<Account> DepositByIdAsync(int id, float amount);
        public Task<Account> WithdrawByIdAsync(int id, float amount);
        public Task<ICollection<Statement>> GetStatementByIdAsync (int id);
        public Task<float> GetBalanceAsync (int id);
    }
}
