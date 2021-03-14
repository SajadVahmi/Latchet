using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Utilities.Services.MessageBus
{
    public interface IMessageConsumer
    {
        void ConsumeEvent(string sender, Parcel parcel);
        void ConsumeCommand(string sender, Parcel parcel);
    }
}
