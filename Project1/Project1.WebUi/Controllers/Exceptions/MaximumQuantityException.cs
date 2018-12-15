using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.WebUi.Controllers.Exceptions
{
    [Serializable()]
    public class MaximumQuantityException : System.Exception
    {
        public MaximumQuantityException() : base() { }
        public MaximumQuantityException(string message) : base(message) { }
        public MaximumQuantityException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected MaximumQuantityException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
