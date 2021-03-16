using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Domain.Data
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        int Commit();
        Task<int> CommitAsync();
    }
}
