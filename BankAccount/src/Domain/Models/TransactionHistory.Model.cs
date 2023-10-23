using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class TransactionHistory
    {
        public TransactionHistory() { }
        public TransactionHistory(int accountId, string dateTime, Operation operation, TransactionStatus transactionStatus) 
        { 
            AccountId = accountId;
            Date = dateTime;
            OperationString = GetOperationString(operation);
            Operation = operation;
            TransactionStatusString = GetTransactionStatusString(transactionStatus);
            TransactionStatus = transactionStatus;
        
        }
        public int Id { get; set; }
        public string Date { get; set; }
        [JsonIgnore]
        public Operation Operation { get; set; }
        [JsonIgnore]
        public TransactionStatus TransactionStatus { get; set; }
        public string OperationString { get; set; }
        public string TransactionStatusString { get; set; }
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