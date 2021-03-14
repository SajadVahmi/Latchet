using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Infrastructures.Events.Outbox
{
    public interface IOutBoxEventItemRepository
    {
        List<OutBoxEventItem> GetOutBoxEventItemsForPublishe(int maxCount = 100);
        void MarkAsRead(List<OutBoxEventItem> outBoxEventItems);
    }
}
