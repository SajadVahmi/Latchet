using Latchet.Infrastructures.Events.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Persistence.Sql.Command.OutBoxEventItems
{
    public class OutBoxEventItemConfig : IEntityTypeConfiguration<OutBoxEventItem>
    {
        public void Configure(EntityTypeBuilder<OutBoxEventItem> builder)
        {
            builder.Property(c => c.AccuredByUserId).HasMaxLength(255);
            builder.Property(c => c.EventName).HasMaxLength(255);
            builder.Property(c => c.AggregateName).HasMaxLength(255);
            builder.Property(c => c.EventTypeName).HasMaxLength(500);
            builder.Property(c => c.AggregateTypeName).HasMaxLength(500);
        }
    }
}
