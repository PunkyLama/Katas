namespace Infrastructure.Models
{
    public class TransactionHistory
    {
        public TransactionHistory() { }
        public TransactionHistory(DateTime dateTime, Operation operation, TransactionStatus transactionStatus) 
        { 
            Date = dateTime;
            Operation = operation;
            TransactionStatus = transactionStatus;
        
        }
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Operation Operation { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
        //public Account Account { get; set; }
        public int AccountId { get; set; }
    }
}