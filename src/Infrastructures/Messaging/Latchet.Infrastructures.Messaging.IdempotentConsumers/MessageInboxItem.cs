using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Infrastructures.Messaging.IdempotentConsumers
{
    public class MessageInboxItem
    {
        public string MessageId { get; set; }
        public string OwnerService { get; set; }

        public DateTime ReceivedDate { get; set; }
    }
}
