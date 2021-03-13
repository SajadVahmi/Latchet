using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Domain.ValueObjects
{
    public abstract record ValueObject<TValueObject>
           where TValueObject : ValueObject<TValueObject>
    {
        protected virtual void CheckInvariants() { }
    }
}
