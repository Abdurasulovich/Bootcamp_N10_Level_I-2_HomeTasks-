using N76_C.Api.DbContexts;
using N76_C.Api.Entities;
using N76_C.Api.Repositories.Interfaces;
using System.Linq.Expressions;

namespace N76_C.Api.Repositories;

public class UserRepository(IdentityDbContext dbContext) : EntityRepositoryBase<User, IdentityDbContext>(dbContext), IUserRepository
{
    public new ValueTask<User> CreateAsync(User user, bool saveChanges, CancellationToken cancellationToken)
    {
        return base.CreateAsync(user, saveChanges, cancellationToken);
    }
    public new ValueTask<User> DeleteAsync(User user, bool saveChanges, CancellationToken cancellationToken)
    {
        return base.DeleteAsync(user, saveChanges, cancellationToken);
    }
    public new ValueTask<User?> DeleteByIdAsync(Guid id, bool saveChanges, CancellationToken cancellationToken)
    {
        return base.DeleteByIdAsync(id, saveChanges, cancellationToken);
    }
    public new ValueTask<User?> GetbyIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default)
    {
        return base.GetByIdAsync(id, asNoTracking, cancellationToken);
    }

    public new IQueryable<User> Get(Expression<Func<User, bool>>? predicate, bool asNoTracking)
    {
        return base.Get(predicate, asNoTracking);
    }

    public new ValueTask<ICollection<User>> GetbyIdsAsync(IEnumerable<Guid> ids, bool asNoTracking = false, CancellationToken cancellationToken = default)
    {
        return base.GetByIdsAsync(ids, asNoTracking, cancellationToken);
    }

    public new ValueTask<User> UpdateAsync(User user, bool saveChanges, CancellationToken cancellationToken)
    {
        return base.UpdateAsync(user, saveChanges, cancellationToken);
    }
}
