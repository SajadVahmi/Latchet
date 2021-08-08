using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Latchet.Domain.Data;
using Latchet.Domain.Entities;
using Latchet.Domain.ValueObjects;
using Latchet.Persistence.Sql.Commands.Dbcontexts;
using Microsoft.EntityFrameworkCore;

namespace Latchet.Persistence.Sql.Commands.Repositories
{
    public class CommandRepository<TEntity,TKey,TDbContext> : ICommandRepository<TEntity,TKey>
        where TEntity : AggregateRoot<TKey>
        where TDbContext : CommandDbContext
    {
        protected readonly TDbContext dbContext;
        public CommandRepository(TDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool Exists(Expression<Func<TEntity, bool>> expression)
        {
            return dbContext.Set<TEntity>().Any(expression);
        }

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await dbContext.Set<TEntity>().AnyAsync(expression,cancellationToken);
        }

        public int SaveChanges()
        {
            return dbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
