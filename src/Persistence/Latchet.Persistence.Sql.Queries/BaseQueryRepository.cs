using Latchet.Domain.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Persistence.Sql.Queries
{
    public class BaseQueryRepository<TDbContext> : IQueryRepository
        where TDbContext : BaseQueryDbContext
    {
        protected readonly TDbContext _dbContext;
        public BaseQueryRepository(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
