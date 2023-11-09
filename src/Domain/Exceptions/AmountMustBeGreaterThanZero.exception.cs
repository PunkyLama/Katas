using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Domain.Exceptions
{
    public class AmountMustBeGreaterThanZero : Exception
    {
        public AmountMustBeGreaterThanZero(float amount) : base($"Amount {amount} must be greater than zero.") { }
        protected AmountMustBeGreaterThanZero(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
