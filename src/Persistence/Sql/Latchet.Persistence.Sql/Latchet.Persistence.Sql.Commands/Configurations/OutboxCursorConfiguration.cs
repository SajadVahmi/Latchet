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
    public class OutboxCursorConfiguration : IEntityTypeConfiguration<OutboxCursor>
    {
        private string schema = string.Empty;

        public OutboxCursorConfiguration(string schema)
        {
            this.schema = schema;
        }

        public void Configure(EntityTypeBuilder<OutboxCursor> builder)
        {
            if (!string.IsNullOrEmpty(this.schema))
                builder.ToTable(nameof(OutboxCursor), this.schema);
            else
                builder.ToTable(nameof(OutboxCursor));

            builder.HasNoKey();
            builder.Property(p => p.Position)
                .HasDefaultValue(0);
        }

    }
}
