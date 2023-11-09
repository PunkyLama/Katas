namespace Domain.Models
{
    public class Statement
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Operation Operation { get; set; }
        public StatementStatus StatementStatus { get; set; }
        public string OperationString { get; private set; }
        public string StatementStatusString { get; private set; }
        public float Amount { get; set; }
        public float OldBalance { get; set; }
        public float? NewBalance { get; set; }

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
        public string GetStatementStatusString(StatementStatus transactionStatus)
        {
            switch (transactionStatus)
            {
                case StatementStatus.Approuved:
                    return "Approuved";
                case StatementStatus.Rejected:
                    return "Rejected";
                default:
                    throw new ArgumentOutOfRangeException(nameof(transactionStatus));
            }
        }
        public Statement CreateRejectedTransaction(Operation operation, float amount, float oldBalance)
        {
            Date = DateTime.Now;
            Operation = operation;
            StatementStatus = StatementStatus.Rejected;
            Amount = amount;
            OldBalance = oldBalance;
            OperationString = GetOperationString(Operation);
            StatementStatusString = GetStatementStatusString(StatementStatus);
            
            return this;
        }
        public Statement CreateApprouvedTransaction(Operation operation, float amount, float oldBalance, float newBalance)
        {
            Date = DateTime.Now;
            Operation = operation;
            StatementStatus = StatementStatus.Approuved;
            Amount = amount;
            OldBalance = oldBalance;
            NewBalance = newBalance;
            OperationString = GetOperationString(Operation);
            StatementStatusString = GetStatementStatusString(StatementStatus);

            return this;
        }
    }
}