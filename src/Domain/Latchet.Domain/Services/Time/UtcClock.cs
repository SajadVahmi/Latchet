using Latchet.Domain.Services.Clock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Domain.Services.Time
{
    public class UtcClock : IClock
    {
        public DateTime Now() => DateTime.UtcNow;
    }
}
