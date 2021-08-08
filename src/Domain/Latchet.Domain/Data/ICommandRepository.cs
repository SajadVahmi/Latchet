using Latchet.Domain.Entities;
using Latchet.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Latchet.Domain.Data
{
    public interface ICommandRepository<TEntity,TKey> : IUnitOfWork
        where TEntity : AggregateRoot<TKey>
    {
        bool Exists(Expression<Func<TEntity, bool>> expression);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);
    }
}
