using Latchet.Utilities.Services.Caching;
using Latchet.Utilities.Services.JsonSerializers;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Infrastructures.Tools.Caching.Microsoft
{
    public class DistributedCacheAdapter : ICacheAdapter
    {
        private readonly IDistributedCache cache;
        private readonly IJsonSerializer serializer;

        public DistributedCacheAdapter(IDistributedCache distributedCache, IJsonSerializer serializer)
        {
            cache = distributedCache;
            this.serializer = serializer;
        }
        public void Add<TInput>(string key, TInput obj)
        {
            cache.Set("", Encoding.UTF8.GetBytes(serializer.Serilize(obj)), new DistributedCacheEntryOptions
            {

            });
            cache.Set(key, Encoding.UTF8.GetBytes(serializer.Serilize(obj)));
        }

        public void Add<TInput>(string key, TInput obj, DateTime? AbsoluteExpiration, TimeSpan? SlidingExpiration)
        {
            cache.Set(key, Encoding.UTF8.GetBytes(serializer.Serilize(obj)), new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = AbsoluteExpiration,
                SlidingExpiration = SlidingExpiration
            });
        }

        public TOutput Get<TOutput>(string key)
        {
            var result = cache.GetString(key);
            return string.IsNullOrWhiteSpace(result) ?
                default : serializer.Deserialize<TOutput>(result);
        }

        public void RemoveCache(string Key)
        {
            throw new NotImplementedException();
        }
    }
}
