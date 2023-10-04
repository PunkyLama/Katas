using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KataBankAccount.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        [Required]
        public string Description { get; set; }
        [ForeignKey(nameof(BankAccount))]
        public int BankAccountId { get; set; }
        public BankAccount BankAccount { get; set; }
    }
}
