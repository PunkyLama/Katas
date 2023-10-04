using System.ComponentModel.DataAnnotations;

namespace KataBankAccount.Models
{
    public class AddBankAccount
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public float Balance { get; set; }
    }
}
