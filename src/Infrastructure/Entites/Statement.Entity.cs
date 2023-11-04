using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities
{
    public class StatementEntity
    {
        public StatementEntity() { }
        [Key]
        public int Id { get; set; }
        public string Date { get; set; }
        public OperationEntity Operation { get; set; }
        public StatementSatusEntity TransactionStatus { get; set; }
        public float Amount { get; set; }
        public float OldBalance { get; set; }
        public float? NewBalance { get; set; }
        public int AccountId { get; set; }
    }
}