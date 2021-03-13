using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Domain.Exceptions
{
    public class InvalidValueObjectStateException : DomainException
    {
        public InvalidValueObjectStateException(string message, params string[] parameters) : base(message)
        {
            Parameters = parameters;
        }
    }
}
