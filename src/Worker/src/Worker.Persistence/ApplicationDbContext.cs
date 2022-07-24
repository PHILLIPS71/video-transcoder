using Giantnodes.Worker.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Giantnodes.Worker.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        public override int SaveChanges()
        {
            AddTimestamps();
            NullifyEmptyStrings();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellation = default)
        {
            AddTimestamps();
            NullifyEmptyStrings();
            return base.SaveChangesAsync(cancellation);
        }

        private void AddTimestamps()
        {
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        break;
                }
            }
        }

        private void NullifyEmptyStrings()
        {
            foreach (var entity in ChangeTracker.Entries())
            {
                var properties = entity
                    .Entity
                    .GetType()
                    .GetProperties()
                    .Where(p => p.PropertyType == typeof(string) && p.CanRead && p.CanWrite);

                foreach (var property in properties)
                {
                    if (string.IsNullOrWhiteSpace(property.GetValue(entity.Entity) as string))
                        property.SetValue(entity.Entity, null, null);
                }
            }
        }
    }
}
