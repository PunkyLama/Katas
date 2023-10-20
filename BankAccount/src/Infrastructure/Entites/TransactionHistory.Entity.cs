using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities
{
    public class TransactionHistoryEntity
    {
        public TransactionHistoryEntity() { }
        [Key]
        public int Id { get; set; }
        public string Date { get; set; }
        public OperationEntity Operation { get; set; }
        public TransactionStatusEntity TransactionStatus { get; set; }
        public int AccountId { get; set; } // Required foreign key property
        public AccountEntity Account { get; set; } = null!; // Required reference navigation to principal
    }
}