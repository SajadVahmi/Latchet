using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Latchet.Domain.Events;

namespace Latchet.Domain.Entities
{
    public interface IAggregateRoot
    {
        IReadOnlyList<IDomainEvent> UncommittedEvents { get; } 
        void ClearUncommittedEvents();
    }
}
