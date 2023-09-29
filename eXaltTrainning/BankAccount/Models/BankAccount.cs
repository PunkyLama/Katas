namespace KataBankAccount.Models
{
    public class BankAccount
    {
        public int Id { get; set; }
        public string Name {  get; set; }
        public float Balance { get; set; }
        public List<string> TransactionHistory { get; set; }        
    }
}
