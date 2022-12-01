using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dinota.Domain
{
    public class QuantityExceedException : Exception
    {
        public QuantityExceedException(string message) : base(message)
        {
        }
    }
}
