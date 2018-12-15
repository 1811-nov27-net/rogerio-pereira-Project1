using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.WebUi.Controllers.Exceptions
{
    [Serializable()]
    public class MinimumStockException : System.Exception
    {
        public MinimumStockException() : base() { }
        public MinimumStockException(string message) : base(message) { }
        public MinimumStockException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected MinimumStockException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
