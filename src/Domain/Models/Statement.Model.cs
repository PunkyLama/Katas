namespace Domain.Models
{
    public class Statement
    {
        public Statement() { }

        /// <summary>
        /// TransactionHistory constructor
        /// </summary>
        /// <param name="dateTime"> Date of the transaction </param>
        /// <param name="operation"> Operation type </param>
        /// <param name="statementStatus"> Status type </param>
        /// <param name="amount"> Amount of the transaction </param>
        /// <param name="oldBalance"> Old balance of the account </param>
        /// <param name="newBalance"> New balance of the account (Optional) </param>
        public Statement(string dateTime, 
            Operation operation, StatementStatus statementStatus, 
            float amount, float oldBalance, float? newBalance = null) 
        {
            Date = dateTime;
            OperationString = GetOperationString(operation);
            Operation = operation;
            StatementStatusString = GetStatementStatusString(statementStatus);
            StatementStatus = statementStatus;
            Amount = amount;
            OldBalance = oldBalance;
            NewBalance = newBalance;
        
        }
        public int Id { get; set; }
        public string Date { get; set; }
        public Operation Operation { get; set; }
        public StatementStatus StatementStatus { get; set; }
        public string OperationString { get; set; }
        public string StatementStatusString { get; set; }
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
    }
}