using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Domain.Exceptions
{
    public class InsufficientFunds : Exception
    {
        public InsufficientFunds(float amount, float balance): base($"Insufficient funds, balance : {balance}, amount : {amount}.") { }
        protected InsufficientFunds(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
