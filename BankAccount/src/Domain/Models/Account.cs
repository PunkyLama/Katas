using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Account
    {
        public Account()
        {
            TransactionHistories = new List<TransactionHistory>();
        }
        [Key]
        public int Id { get; set; }
        public User User { get; set; }
        [ForeignKey(nameof(User))]
        public int? UserId { get; set; }
        public int Balance { get; set; }
        public ICollection<TransactionHistory> TransactionHistories { get; set; }
    }
}
