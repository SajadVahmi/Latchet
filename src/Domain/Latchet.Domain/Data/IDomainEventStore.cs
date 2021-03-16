using Latchet.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Domain.Data
{
    public interface IDomainEventStore
    {
        void Save<TEvent>(string aggregateName, string aggregateId, IEnumerable<TEvent> events) where TEvent : IDomainEvent;
        Task SaveAsync<TEvent>(string aggregateName, string aggregateId, IEnumerable<TEvent> events) where TEvent : IDomainEvent;
    }
}
