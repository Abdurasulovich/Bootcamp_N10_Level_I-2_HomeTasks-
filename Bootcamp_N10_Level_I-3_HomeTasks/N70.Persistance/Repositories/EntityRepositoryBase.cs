using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using N70.Domain.Common;
using N70.Persistance.Repositories.Interfaces;

namespace N70.Persistance.Repositories;

public abstract class EntityRepositoryBase<TEntity, TContext> where TEntity : class, IEntity where TContext : DbContext
{
    protected TContext DbContext => (TContext)_dbContext;
    private readonly DbContext _dbContext;

    public EntityRepositoryBase(DbContext context)
    {
        _dbContext = context;
    }
    public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>>? predicate = default, bool asNoTracking = false)
    {
        var initialQuery = DbContext.Set<TEntity>().Where(entity => true);

        if (predicate is not null)
            initialQuery = initialQuery.Where(predicate);

        if (asNoTracking)
            initialQuery = initialQuery.AsNoTracking();

        return initialQuery;
    }

    public async ValueTask<TEntity?> GetByIdAsync(
        Guid Id, 
        bool asNoTracking = false, 
        CancellationToken cancellationToken = default)
    {
        var initialQuery = DbContext.Set<TEntity>().Where(entity => true);
        if(asNoTracking)
            initialQuery = initialQuery.AsNoTracking();

        return await initialQuery.SingleOrDefaultAsync(entity => entity.Id == Id,  cancellationToken: cancellationToken);
    }

    public async ValueTask<IList<TEntity>> GetByIdsAsync(
        IEnumerable<Guid> Ids, 
        bool asNoTracking = false, 
        CancellationToken cancellationToken = default)
    {
        var initialQuery = _dbContext.Set<TEntity>().Where(entity => true);

        if (asNoTracking)
            initialQuery = initialQuery.AsNoTracking();

        initialQuery = initialQuery.Where(entity => Ids.Contains(entity.Id));

        return await initialQuery.ToListAsync(cancellationToken: cancellationToken);
    }

    public async ValueTask<TEntity> CreateAsync(
        TEntity entity, 
        bool saveChanges = true, 
        CancellationToken cancellationToken = default)
    {
        await DbContext.Set<TEntity>().AddAsync(entity, cancellationToken);

        if (saveChanges)
            await DbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public async ValueTask<TEntity> UpdateAsync(
        TEntity entity, 
        bool saveChanges = true, 
        CancellationToken cancellationToken = default)
    {
        var type = typeof(TEntity);

        DbContext.Set<TEntity>().Update(entity);

        if (saveChanges)
            await DbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async ValueTask<TEntity?> DeleteAsync(
        TEntity entity, 
        bool saveChanges = true, 
        CancellationToken cancellationToken = default)
    {
        DbContext.Set<TEntity>().Remove(entity);

        if (saveChanges)
            await _dbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public async ValueTask<TEntity?> DeleteByIdAsync(
        Guid Id, 
        bool saveChanges = true, 
        CancellationToken cancellationToken = default)
    {
        var entity =
            await DbContext.Set<TEntity>().FirstOrDefaultAsync(entity => entity.Id == Id, cancellationToken) ??
            throw new InvalidOperationException();
        DbContext.Set<TEntity>().Remove(entity);
        if (saveChanges)
            await DbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }
}