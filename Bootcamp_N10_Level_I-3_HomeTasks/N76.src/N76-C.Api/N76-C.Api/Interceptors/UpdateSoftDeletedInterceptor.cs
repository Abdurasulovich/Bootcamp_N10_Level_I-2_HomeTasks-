using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using N76_C.Api.Models.Common.Interfaces;

namespace N76_C.Api.Interceptors;

public class UpdateSoftDeletedInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        var softDeletedEntities = eventData.Context.ChangeTracker.Entries<ISoftDeletedEntity>().ToList();

        softDeletedEntities.ForEach(entry =>
        {
            if (entry.State == Microsoft.EntityFrameworkCore.EntityState.Deleted)
            {
                entry.Property(nameof(ISoftDeletedEntity.DeletedTime)).CurrentValue = DateTime.UtcNow;
                entry.Property(nameof(ISoftDeletedEntity.IsDeleted)).CurrentValue = true;
                entry.State = EntityState.Modified;
            }
        });

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
