using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports
{
    public interface ITransactionHistoryPort
    {
        public Task<TransactionHistory> CreateTransactionHistoryAsync(TransactionHistory transactionHistory);
        public Task<TransactionHistory> GetTransactionHistoryAsync(int id);        
    }
}
