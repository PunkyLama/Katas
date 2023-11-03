using Domain.Injection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Queries
{
    public class GetStatement : IRequest<TransactionHistory>
    {
        public int Id { get; set; }
    }
}
