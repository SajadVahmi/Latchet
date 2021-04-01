using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Domain.Services.ObjectMappers
{
    public interface IObjectMapper
    {
        TDestination Map<TSource, TDestination>(TSource source);
    }
}
