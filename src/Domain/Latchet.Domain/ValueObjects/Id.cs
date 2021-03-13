using Latchet.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Domain.ValueObjects
{
    public record Id : ValueObject<Id>
    {
        public static Id FromString(string value) => new Id(value);
        public static Id FromGuid(Guid value) => new Id { Value = value };
        public Id(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidValueObjectStateException("ValidationErrorIsRequire", nameof(Id));
            }
            if (Guid.TryParse(value, out Guid tempValue))
            {
                Value = tempValue;
            }
            else
            {
                throw new InvalidValueObjectStateException("ValidationErrorInvalidValue", nameof(Id));
            }
        }
        private Id()
        {

        }

        public Guid Value { get; private set; }

        
        public override string ToString()
        {
            return Value.ToString();
        }

        public static explicit operator string(Id title) => title.Value.ToString();
        public static implicit operator Id(string value) => new Id(value);


        public static explicit operator Guid(Id title) => title.Value;
        public static implicit operator Id(Guid value) => new Id { Value = value };

    }
}
