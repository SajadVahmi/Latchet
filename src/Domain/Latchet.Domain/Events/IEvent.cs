using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Domain.Events
{
    public interface IEvent
    {
        Guid EventId { get; }
        DateTime PublishDateTime { get; }
    }
}
