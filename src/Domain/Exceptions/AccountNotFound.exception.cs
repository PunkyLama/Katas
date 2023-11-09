using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Domain.Exceptions
{
    public class AccountNotFound : Exception
    {
        public AccountNotFound(int Id): base($"Account with id {Id} not found.") { }
        protected AccountNotFound(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
