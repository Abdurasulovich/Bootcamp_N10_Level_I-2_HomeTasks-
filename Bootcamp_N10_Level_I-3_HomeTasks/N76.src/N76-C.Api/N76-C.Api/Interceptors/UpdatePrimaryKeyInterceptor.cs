using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using N76_C.Api.Models.Common.Interfaces;

namespace N76_C.Api.Interceptors;

public class UpdatePrimaryKeyInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData,
                                                            InterceptionResult<int> result)
    {
        var auditableEntities = eventData.Context!.ChangeTracker.Entries<IEntity>().ToList();

        auditableEntities.ForEach(entry =>
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(nameof(IEntity.Id)).CurrentValue = Guid.NewGuid();
            }
        });

        return base.SavingChanges(eventData, result);
    }
}
