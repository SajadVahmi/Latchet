using Latchet.Domain.Exceptions;
using Latchet.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Domain.ValueObjects
{
    public record NationalId : ValueObject<NationalId>
    {
       
        public static NationalId FromString(string value) => new NationalId(value);
        
        public NationalId(string value)
        {
            if (!value.IsValidNationalCode())
            {
                throw new InvalidValueObjectStateException("Invalid value", nameof(NationalId));
            }
            Value = value;
        }
        private NationalId()
        {

        }

        public string Value { get; private set; }
        public override string ToString()
        {
            return Value.ToString();
        }

        public static explicit operator string(NationalId nationalId) => nationalId.Value.ToString();
        public static implicit operator NationalId(string value) => new NationalId(value);

    }
}

