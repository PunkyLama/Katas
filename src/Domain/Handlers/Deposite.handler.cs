using Domain.Commands;
using Domain.Injection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Handlers
{
    public class Deposite : IHandler<Deposit, Account>
    {
        public Task<Account> HandleAsync(Deposit request)
        {
            throw new NotImplementedException();
        }
    }
}
