using Latchet.Persistence.Sql.Commands.Configurations;
using Latchet.Persistence.Sql.Commands.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Persistence.Sql.Commands.Dbcontexts
{
    public abstract class CommandDbContext:DbContext
    {
        public CommandDbContext(DbContextOptions options)
            : base(options)
        {
          
        }
        public string Schema { get; protected set; }
        public DbSet<OutboxItem> OutboxItems { get; set; }
        public abstract string DefineSchema();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new OutBoxItemConfiguration(Schema));
            builder.ApplyConfiguration(new OutboxCursorConfiguration(Schema));
        }

        
    }
}
