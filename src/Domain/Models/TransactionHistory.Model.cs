using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class TransactionHistory
    {
        public TransactionHistory() { }

        /// <summary>
        /// TransactionHistory constructor
        /// </summary>
        /// <param name="accountId"> Account Id </param>
        /// <param name="dateTime"> Date of the transaction </param>
        /// <param name="operation"> Operation type </param>
        /// <param name="transactionStatus"> Status type </param>
        /// <param name="amount"> Amount of the transaction </param>
        /// <param name="oldBalance"> Old balance of the account </param>
        /// <param name="newBalance"> New balance of the account (Optional) </param>
        public TransactionHistory(int accountId, string dateTime, 
            Operation operation, TransactionStatus transactionStatus, 
            float amount, float oldBalance, float? newBalance = null) 
        { 
            AccountId = accountId;
            Date = dateTime;
            OperationString = GetOperationString(operation);
            Operation = operation;
            TransactionStatusString = GetTransactionStatusString(transactionStatus);
            TransactionStatus = transactionStatus;
            Amount = amount;
            OldBalance = oldBalance;
            NewBalance = newBalance;
        
        }
        public int Id { get; set; }
        public string Date { get; set; }
        [JsonIgnore]
        public Operation Operation { get; set; }
        [JsonIgnore]
        public TransactionStatus TransactionStatus { get; set; }
        public string OperationString { get; set; }
        public string TransactionStatusString { get; set; }
        public float Amount { get; set; }
        public float OldBalance { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public float? NewBalance { get; set; }
        public int AccountId { get; set; }


        public string GetOperationString(Operation operation)
        {
            switch (operation)
            {
                case Operation.Deposit:
                    return "Deposit";
                case Operation.Withdraw:
                    return "Withdraw";
                default:
                    throw new ArgumentOutOfRangeException(nameof(operation));
            }
        }

        public string GetTransactionStatusString(TransactionStatus transactionStatus)
        {
            switch (transactionStatus)
            {
                case TransactionStatus.Approuved:
                    return "Approuved";
                case TransactionStatus.Rejected:
                    return "Rejected";
                default:
                    throw new ArgumentOutOfRangeException(nameof(transactionStatus));
            }
        }
    }
}