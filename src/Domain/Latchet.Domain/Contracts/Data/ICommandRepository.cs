using Latchet.Domain.Entities;
using Latchet.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Domain.Contracts.Data
{
    public interface ICommandRepository<TEntity> : IUnitOfWork
        where TEntity : AggregateRoot
    {
        void Delete(TEntity entity);
        void Insert(TEntity entity);
        TEntity Get(Id id);
        TEntity GetGraph(Id id);
        bool Exists(Expression<Func<TEntity, bool>> expression);
    }
}
