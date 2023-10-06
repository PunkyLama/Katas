using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class TransactionHistory
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public Account Account { get; set; }
        [ForeignKey(nameof(Account))]
        public int AccountId { get; set; }
    }
}