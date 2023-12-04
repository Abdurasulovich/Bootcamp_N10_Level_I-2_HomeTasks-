using Caching.SimpleInfra.Domain.Common.Caching;
using Caching.SimpleInfra.Domain.Entities;
using Caching.SimpleInfra.Persistence.Caching;
using Caching.SimpleInfra.Persistence.DataContexts;
using Caching.SimpleInfra.Persistence.Repostiories.Interfaces;
using System.Linq.Expressions;

namespace Caching.SimpleInfra.Persistence.Repostiories;

public class UserRepository(IdentityDbContext dbContext, ICacheBroker cacheBroker) : EntityRepositoryBase<User, IdentityDbContext>(
    dbContext,
    cacheBroker, 
    new CacheEntryOptions
    //new CacheEntryOptions(TimeSpan.FromMinutes(10), TimeSpan.FromMinutes(3))
), IUserRepository
{
    public new ValueTask<User> CreateAsync(User user, bool saveChanges, CancellationToken cancellationToken) =>
        base.CreateAsync(user, saveChanges, cancellationToken);

    public new ValueTask<User?> DeleteByIdAsync(Guid userId, bool saveChanges, CancellationToken cancellationToken) =>
        base.DeleteByIdAsync(userId, saveChanges, cancellationToken);

    public new IQueryable<User> Get(Expression<Func<User, bool>>? predicate, bool asNoTracking) =>
        base.Get(predicate, asNoTracking);

    public new ValueTask<User?> GetByIdAsync(Guid userId, bool asNoTracking, CancellationToken cancellationToken)=>
        base.GetByIdAsync(userId, asNoTracking, cancellationToken);

    public new ValueTask<User> UpdateAsync(User user, bool saveChanges, CancellationToken cancellationToken)=>
        base.UpdateAsync(user, saveChanges, cancellationToken);
}
