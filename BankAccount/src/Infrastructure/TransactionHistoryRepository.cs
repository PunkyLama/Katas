using Domain.Models;
using Domain.Ports;

namespace Infrastructure
{
    public class TransactionHistoryRepository : ITransactionHistoryPort
    {
        public TransactionHistoryRepository()
        {
        }

        public Task<TransactionHistory> CreateTransactionHistoryAsync(TransactionHistory transactionHistory)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionHistory> GetTransactionHistoryAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
