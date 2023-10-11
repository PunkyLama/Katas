using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands
{
    public class DepositCommand : IRequest<Account>
    {
        public int Id { get; set; }
        public float Amount { get; set; }

        public DepositCommand(int id, float amount)
        {
            Id = id;
            this.Amount = amount;
        }
    }
}
