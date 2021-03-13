using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Domain.Exceptions
{
    public abstract class DomainException : Exception
    {
        public string[] Parameters { get; set; }

        public DomainException(string message, params string[] parameters) : base(message)
        {
            Parameters = parameters;
        }
    }
}
