using Latchet.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Application.Queries
{
    public sealed class QueryResult<TData> : Result
    {
        internal TData data;
        public TData Data
        {
            get
            {
                return data;
            }
        }
    }
}
