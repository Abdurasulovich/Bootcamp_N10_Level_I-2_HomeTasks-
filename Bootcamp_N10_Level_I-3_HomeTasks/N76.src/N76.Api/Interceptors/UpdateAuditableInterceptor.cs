using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using N76.Api.Models.Common;

namespace N76.Api.Interceptors;

public class UpdateAuditableInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        var auditableEntities = eventData.Context!.ChangeTracker.Entries<IAuditableEntity>().ToList();

        auditableEntities.ForEach(entry =>
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(nameof(IAuditableEntity.CreatedTime)).CurrentValue = DateTime.UtcNow;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Property(nameof(IAuditableEntity.ModifiedTime)).CurrentValue = DateTime.UtcNow;
            }
        });

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
