using Latchet.Domain.Extensions;
using Latchet.Domain.Services.AuthenticatedUserService;
using Latchet.Domain.Services.JsonSerializer;
using Latchet.Persistence.Sql.Commands.Configurations;
using Latchet.Persistence.Sql.Commands.Extensions;
using Latchet.Persistence.Sql.Commands.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
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

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            beforeSaveTriggers();
            ChangeTracker.AutoDetectChangesEnabled = false;
            var result = base.SaveChanges();
            ChangeTracker.AutoDetectChangesEnabled = true;
            return result;

        }
        public override Task<int> SaveChangesAsync(
         bool acceptAllChangesOnSuccess,
         CancellationToken cancellationToken = default)
        {
            ChangeTracker.DetectChanges();
            beforeSaveTriggers();
            ChangeTracker.AutoDetectChangesEnabled = false;
            var result = base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            ChangeTracker.AutoDetectChangesEnabled = true;
            return result;
        }

        private void beforeSaveTriggers()
        {
            cleanString();
            addOutboxEvetItems();
        }
        private void cleanString()
        {
            var changedEntities = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);
            foreach (var item in changedEntities)
            {
                if (item.Entity == null)
                    continue;

                var properties = item.Entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.CanRead && p.CanWrite && p.PropertyType == typeof(string));

                foreach (var property in properties)
                {
                    var propName = property.Name;
                    var val = (string)property.GetValue(item.Entity, null);

                    if (val.HasValue())
                    {
                        var newVal = val.CleanString();
                        if (newVal == val)
                            continue;
                        property.SetValue(item.Entity, newVal, null);
                    }
                }
            }
        }
        private void addOutboxEvetItems()
        {
            var changedAggregates = ChangeTracker.GetAggregatesWithEvent();
            var userInfoService = this.GetService<IAuthenticatedUserService>();
            var jsonSerializer = this.GetService<IJsonSerializer>();

            foreach (var aggregate in changedAggregates)
            {
                var events = aggregate.UncommittedEvents;
                foreach (var @event in events)
                {
                    var outboxItem = new OutboxItem();
                    outboxItem.EventId = @event.EventId;
                    outboxItem.AccuredByUserId = userInfoService.GetId();
                    outboxItem.AccuredOn = DateTime.UtcNow;
                    outboxItem.AggregateId = aggregate.Id.ToString();
                    outboxItem.AggregateName = aggregate.GetType().Name;
                    outboxItem.AggregateTypeName = aggregate.GetType().FullName;
                    outboxItem.EventName = @event.GetType().Name;
                    outboxItem.EventTypeName = @event.GetType().FullName;
                    outboxItem.EventBody = jsonSerializer.Serilize(@event);
                    OutboxItems.Add(outboxItem);
                }
                aggregate.ClearUncommittedEvents();
            }
        }

        public static void AddRestrictDeleteBehaviorConvention(ModelBuilder modelBuilder)
        {
            IEnumerable<IMutableForeignKey> cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
            foreach (IMutableForeignKey fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;
        }

    }
}
