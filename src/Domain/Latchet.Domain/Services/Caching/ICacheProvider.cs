using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Domain.Services.Caching
{
    public interface ICacheProvider
    {
        void Add<TInput>(string key, TInput obj, DateTime? AbsoluteExpiration, TimeSpan? SlidingExpiration);
        TOutput Get<TOutput>(string key);
        void RemoveCache(string key);
    }
}
