using Caching.SimpleInfra.Domain.Common.Caching;
using Caching.SimpleInfra.Domain.Common.Entities;
using Caching.SimpleInfra.Persistence.Caching;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Caching.SimpleInfra.Persistence.Repostiories;

public abstract class EntityRepositoryBase<TEntity, TContext>(TContext dbContext, ICacheBroker cacheBroker, CacheEntryOptions? cacheEntryOptions = default)
    where TEntity : class, IEntity where TContext : DbContext
{
    protected TContext DbContext => dbContext;

    protected IQueryable<TEntity> Get(
        Expression<Func<TEntity, bool>>? predicate,
        bool asNoTracking = false
        )
    {
        var initialQuery = DbContext.Set<TEntity>().Where(entity => true);

        if (predicate is not null)
            initialQuery = initialQuery.Where(predicate);

        if (asNoTracking)
            initialQuery = initialQuery.AsNoTracking();

        return initialQuery;
    }

    protected async ValueTask<TEntity?> GetByIdAsync(
        Guid userId,
        bool asNoTracking = false,
        CancellationToken cancellationToken=default
        )
    {
        var foundEntity = default(TEntity?);

        if(cacheEntryOptions is null || !await cacheBroker.TryGetAsync<TEntity>(userId.ToString(), out var cachedEntity))
        {
            var initialQuery = DbContext.Set<TEntity>().AsQueryable();
            if (asNoTracking)
                initialQuery = initialQuery.AsNoTracking();

            foundEntity = await initialQuery.FirstOrDefaultAsync(entity=>entity.Id == userId,  cancellationToken: cancellationToken);

            if (foundEntity is not null && cacheEntryOptions is not null)
                await cacheBroker.SetAsync(foundEntity.Id.ToString(), foundEntity, cacheEntryOptions);
        }

        return await cacheBroker.GetOrSetAsync(
            userId.ToString(),
            () =>
            {
                

            }, cacheEntryOptions
            );
    }

    protected async ValueTask<IList<TEntity>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        bool asNoTracking = false,
        CancellationToken cancellationToken=default
        )
    {
        var initialQuery = DbContext.Set<TEntity>().Where(entity => true);

        if(asNoTracking)
            initialQuery=initialQuery.AsNoTracking();

        return await initialQuery.ToListAsync(cancellationToken: cancellationToken);
    }

    protected async ValueTask<TEntity> CreateAsync(
        TEntity entity,
        bool saveChanges= true,
        CancellationToken cancellationToken = default
        )
    {
        await DbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
        await cacheBroker.SetAsync(entity.Id.ToString(), entity, cacheEntryOptions);

        if (saveChanges)
            await DbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    protected async ValueTask<TEntity> UpdateAsync(
        TEntity entity,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
        )
    {
        DbContext.Set<TEntity>().Update(entity);
        await cacheBroker.SetAsync(entity.Id.ToString(), entity, cacheEntryOptions);

        if (saveChanges) await DbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    protected async ValueTask<TEntity?> DeleteAsync(
        TEntity entity,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
        )
    {
        DbContext.Set<TEntity>().Remove(entity);
        await cacheBroker.DeleteAsync(entity.Id.ToString());

        if (saveChanges)
            await DbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    protected async ValueTask<TEntity?> DeleteByIdAsync(
        Guid id,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
        )
    {
        var entity = await DbContext.Set<TEntity>().FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken) ??
            throw new InvalidOperationException();

        DbContext.Set<TEntity>().Remove(entity);
        await cacheBroker.DeleteAsync(entity.Id.ToString());
        if (saveChanges)
            await DbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }
}
