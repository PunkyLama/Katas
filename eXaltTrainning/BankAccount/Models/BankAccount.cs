using System.ComponentModel.DataAnnotations;

namespace KataBankAccount.Models
{
    public class BankAccount
    {
        [Key]
        public int Id { get; set; }
        public string Name {  get; set; }
        public float Balance { get; set; }
        public List<History> TransactionHistory { get; set; }        
    }


    public class History
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
