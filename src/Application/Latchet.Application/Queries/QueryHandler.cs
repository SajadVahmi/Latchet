using Latchet.Application.Common;
using Latchet.Domain.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Application.Queries
{

    public abstract class QueryHandler<TQuery, TData> : IQueryHandler<TQuery, TData>
        where TQuery : class, IQuery<TData>
    {
        protected readonly LatchetServices latchetServices;
        protected readonly QueryResult<TData> result = new QueryResult<TData>();

        protected virtual Task<QueryResult<TData>> ResultAsync(TData data, ResultStatus status)
        {
            result.data = data;
            result.Status = status;
            return Task.FromResult(result);
        }

        protected virtual QueryResult<TData> Result(TData data, ResultStatus status)
        {
            result.data = data;
            result.Status = status;
            return result;
        }


        protected virtual Task<QueryResult<TData>> ResultAsync(TData data)
        {
            var status = data != null ? ResultStatus.Ok : ResultStatus.NotFound;
            return ResultAsync(data, status);
        }

        protected virtual QueryResult<TData> Result(TData data)
        {
            var status = data != null ? ResultStatus.Ok : ResultStatus.NotFound;
            return Result(data, status);
        }

        public QueryHandler(LatchetServices latchetServices)
        {
            this.latchetServices = latchetServices;
        }

        public abstract Task<QueryResult<TData>> Handle(TQuery request);
    }
}
