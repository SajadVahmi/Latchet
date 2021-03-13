using Latchet.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Domain.Entities
{
    public class AggregateRoot:Entity
    {
        private readonly List<IDomainEvent> _events;
        protected AggregateRoot() => _events = new List<IDomainEvent>();
        public AggregateRoot(IEnumerable<IDomainEvent> events)
        {
            if (events == null) return;
            foreach (var @event in events)
                ((dynamic)this).On((dynamic)@event);
        }
        protected void AddEvent(IDomainEvent @event) => _events.Add(@event);
        public IEnumerable<IDomainEvent> GetEvents() => _events.AsEnumerable();
        public void ClearEvents() => _events.Clear();
    }
}
