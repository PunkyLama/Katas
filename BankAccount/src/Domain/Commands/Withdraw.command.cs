using Domain.Injection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands
{
    public class Withdraw : IRequest<Account>
    {
        public int Id { get; set; }
        public float Amount { get; set; }

        public Withdraw(int id, float amount) 
        {
            this.Id = id;
            this.Amount = amount;
        }
    }
}
