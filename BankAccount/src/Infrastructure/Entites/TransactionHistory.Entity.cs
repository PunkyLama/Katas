namespace Infrastructure.Entities
{
    public class TransactionHistoryEntity
    {
        public TransactionHistoryEntity() { }
        public TransactionHistoryEntity(DateTime dateTime, OperationEntity operation, TransactionStatusEntity transactionStatus) 
        { 
            Date = dateTime;
            Operation = operation;
            TransactionStatus = transactionStatus;
        
        }
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public OperationEntity Operation { get; set; }
        public TransactionStatusEntity TransactionStatus { get; set; }
        //public Account Account { get; set; }
        public int AccountId { get; set; }
    }
}