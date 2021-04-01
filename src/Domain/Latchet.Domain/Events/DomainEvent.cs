using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Domain.Events
{
    public class DomainEvent : IDomainEvent
    {
        public Guid EventId { get; }
        public DateTime PublishDateTime { get; }
        public DomainEvent()
        {
            EventId = Guid.NewGuid();
            PublishDateTime = DateTime.Now;
        }
    }
}
