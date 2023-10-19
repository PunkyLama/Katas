namespace Infrastructure.Entities
{
    public class AccountEntity
    {
        public AccountEntity()
        {
            TransactionHistories = new List<TransactionHistoryEntity>();
        }
        public int Id { get; set; }
        public float Balance { get; set; }
        public ICollection<TransactionHistoryEntity> TransactionHistories { get; set; }
    }
}
