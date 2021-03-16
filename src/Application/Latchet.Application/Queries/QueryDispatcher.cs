using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Application.Queries
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceScopeFactory serviceFactory;
        public QueryDispatcher(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceFactory = serviceScopeFactory;
        }

        #region Query Dispatcher

        public Task<QueryResult<TData>> Execute<TQuery, TData>(TQuery query) where TQuery : class, IQuery<TData>
        {
            using var serviceScope = serviceFactory.CreateScope();
            var handler = serviceScope.ServiceProvider.GetRequiredService<IQueryHandler<TQuery, TData>>();
            return handler.Handle(query);
        }
        #endregion


    }
}
