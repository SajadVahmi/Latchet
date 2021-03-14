using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Utilities.Configurations
{
    public class ApplicationEvents
    {
        public bool TransactionalEventsEnabled { get; set; }
        public bool RaiseInmemoryEvents { get; set; }
    }
}
