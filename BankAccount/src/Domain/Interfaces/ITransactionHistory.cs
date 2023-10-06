using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    internal interface ITransactionHistory
    {
        public Task<TransactionHistory> CreateTransactionHistoryAsync(TransactionHistory transactionHistory);
        public Task<TransactionHistory> GetTransactionHistoryAsync(int id);        
    }
}
