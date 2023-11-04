using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities
{
    public class AccountEntity
    {        
        [Key]
        public int Id { get; set; }
        public float Balance { get; set; }
    }
}
