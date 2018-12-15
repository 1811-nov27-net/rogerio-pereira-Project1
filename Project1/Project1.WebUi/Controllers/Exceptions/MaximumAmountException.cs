using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.WebUi.Controllers.Exceptions
{
    [Serializable()]
    public class MaximumAmountException : System.Exception
    {
        public MaximumAmountException() : base() { }
        public MaximumAmountException(string message) : base(message) { }
        public MaximumAmountException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected MaximumAmountException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
