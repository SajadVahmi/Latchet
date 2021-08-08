using Latchet.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Domain.Entities
{
    public class  AggregateRoot<TKey>:Entity<TKey>
    {
        private List<IDomainEvent> uncommittedEvents;
        public IReadOnlyList<IDomainEvent> UncommittedEvents => uncommittedEvents.AsReadOnly();
        public void ClearUncommittedEvents() => uncommittedEvents.Clear();
        public AggregateRoot()
        {
            uncommittedEvents = new List<IDomainEvent>();
        }

    }
}
