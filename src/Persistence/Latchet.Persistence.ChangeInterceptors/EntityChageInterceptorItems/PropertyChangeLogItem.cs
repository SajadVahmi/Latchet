using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Persistence.ChangeInterceptors.EntityChageInterceptorItems
{
    public class PropertyChangeLogItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ChageInterceptorItemId { get; set; }
        public string PropertyName { get; set; }
        public string Value { get; set; }
    }
}
