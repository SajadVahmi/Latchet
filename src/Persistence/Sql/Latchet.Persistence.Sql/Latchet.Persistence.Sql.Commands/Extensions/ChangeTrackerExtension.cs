using Latchet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Persistence.Sql.Commands.Extensions
{
    public static class ChangeTrackerExtension
    {
        public static List<AggregateRoot> GetAggregatesWithEvent(this ChangeTracker changeTracker) =>
           changeTracker.Entries<AggregateRoot>()
                                    .Where(x => x.State != EntityState.Detached).Select(c => c.Entity).Where(c => c.UncommittedEvents.Any()).ToList();
    }
}
