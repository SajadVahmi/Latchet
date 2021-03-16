using Latchet.Utilities.Services.Caching;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Infrastructures.Tools.Caching.Microsoft
{
    public class InMemoryCacheAdapter : ICacheAdapter
    {
        private readonly IMemoryCache memoryCache;


        public InMemoryCacheAdapter(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }


        public void Add<TInput>(string key, TInput obj, DateTime? AbsoluteExpiration, TimeSpan? SlidingExpiration)
        {
            memoryCache.Set(key, obj, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = AbsoluteExpiration,
                SlidingExpiration = SlidingExpiration
            });
        }

        public TOutput Get<TOutput>(string key)
        {
            var result = memoryCache.TryGetValue(key, out TOutput resultObject);
            return resultObject;

        }

        public void RemoveCache(string key)
        {
            memoryCache.Remove(key);
        }
    }
}
