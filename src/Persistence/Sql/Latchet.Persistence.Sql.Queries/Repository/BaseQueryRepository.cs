using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Latchet.Domain.Data;
using Latchet.Persistence.Sql.Queries.DbContexts;

namespace Latchet.Persistence.Sql.Queries.Repository
{
    public class BaseQueryRepository<TDbContext> : IQueryRepository
        where TDbContext : QueryDbContext
    {
        protected readonly TDbContext dbContext;
        public BaseQueryRepository(TDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
