using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Utilities.Services.MessageBus
{
    public class Parcel
    {
        public string MessageId { get; set; }
        public string CorrelationId { get; set; }
        public string MessageName { get; set; }
        public Dictionary<string, object> Headers { get; set; }
        public string MessageBody { get; set; }
        public string Route { get; set; }
    }
}
