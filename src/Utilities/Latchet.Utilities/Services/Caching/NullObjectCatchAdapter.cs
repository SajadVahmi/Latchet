using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Utilities.Services.Caching
{
    public class NullObjectCacheAdapter : ICacheAdapter
    {
        public void Add<TInput>(string key, TInput obj, DateTime? AbsoluteExpiration, TimeSpan? SlidingExpiration) { }

        public TOutput Get<TOutput>(string key) => default;


        public void RemoveCache(string key) { }
    }
}
