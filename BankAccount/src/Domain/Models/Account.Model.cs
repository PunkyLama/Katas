namespace Domain.Models
{
    public class Account
    {
        public Account()
        {
            TransactionHistories = new List<TransactionHistory>();
        }
        public int Id { get; set; }
        public float Balance { get; set; }
        public ICollection<TransactionHistory> TransactionHistories { get; set; }
    }
}
