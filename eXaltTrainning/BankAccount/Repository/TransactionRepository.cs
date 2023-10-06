using KataBankAccount.Models;
using System.Text;

namespace KataBankAccount.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private StringBuilder _history = new StringBuilder();
        public Transaction AddTransaction(int id, Task<BankAccount>? entity = default, float amount = default, float balance = default)
        {
            switch (id)
            {
                case 1:
                    _history.Append($"[{DateTime.Now}] The account has been created.");
                    break;
                case 2:
                    _history.Append($"[{DateTime.Now}] Deposit of {amount}€, old amount : {balance}€, new amount : {entity.Result.Balance}€.");
                    break;
                case 3:
                    _history.Append($"[{DateTime.Now}] Withdraw of {amount}€, old amount : {balance}€, new amount : {entity.Result.Balance}€.");
                    break;
                case 4:
                    _history.Append($"[{DateTime.Now}] The account balance has been consulted.");
                    break;
            }

            var historic = new Transaction()
            {
                Description = _history.ToString()
            };
            _history.Clear();
            return historic;
        }
    }
}
