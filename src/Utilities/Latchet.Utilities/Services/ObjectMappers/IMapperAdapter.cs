using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Utilities.Services.ObjectMappers
{
    public interface IMapperAdapter
    {
        TDestination Map<TSource, TDestination>(TSource source);
    }
}
