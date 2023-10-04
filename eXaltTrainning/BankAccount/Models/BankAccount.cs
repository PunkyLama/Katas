using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KataBankAccount.Models
{
    public class BankAccount
    {
        [Key]
        public int BankAccountId { get; set; }
        [Required]
        public string Name {  get; set; }
        [Required]
        public float Balance { get; set; }
        [ForeignKey("BankAccountId")]
        public ICollection<Transaction>? TransactionHistory { get; set; }       

    }
    
}
