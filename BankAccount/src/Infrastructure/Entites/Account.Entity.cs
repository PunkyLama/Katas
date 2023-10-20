using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities
{
    public class AccountEntity
    {
        
        public AccountEntity()
        {
            TransactionHistories = new List<TransactionHistoryEntity>();
        }
        
        [Key]
        public int Id { get; set; }
        public float Balance { get; set; }
        public ICollection<TransactionHistoryEntity> TransactionHistories { get; set; }
    }
}
