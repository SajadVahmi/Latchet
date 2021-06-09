using Latchet.Persistence.Sql.Commands.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Persistence.Sql.Commands.Configurations
{
    public class OutBoxItemConfiguration : IEntityTypeConfiguration<OutboxItem>
    {
        private string schema = string.Empty;

        public OutBoxItemConfiguration(string schema)
        {
            this.schema = schema;
        }
        public void Configure(EntityTypeBuilder<OutboxItem> builder)
        {
            if (!string.IsNullOrEmpty(this.schema))
                builder.ToTable(nameof(OutboxItem), this.schema);
            else
                builder.ToTable(nameof(OutboxItem));
            builder.HasKey(c => c.Id);
            builder.Property(c => c.AccuredByUserId).HasMaxLength(255);
            builder.Property(c => c.EventName).HasMaxLength(255);
            builder.Property(c => c.AggregateName).HasMaxLength(255);
            builder.Property(c => c.EventTypeName).HasMaxLength(500);
            builder.Property(c => c.AggregateTypeName).HasMaxLength(500);
        }
    }
}
