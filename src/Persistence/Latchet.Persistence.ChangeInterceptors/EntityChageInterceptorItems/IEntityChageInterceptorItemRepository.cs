using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Persistence.ChangeInterceptors.EntityChageInterceptorItems
{
    public interface IEntityChageInterceptorItemRepository
    {
        public void Save(List<EntityChageInterceptorItem> entityChageInterceptorItems);
        public Task SaveAsync(List<EntityChageInterceptorItem> entityChageInterceptorItems);
    }
}
