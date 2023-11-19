using N70.Domain.Entities;
using N70.Persistance.DataContexts;
using N70.Persistance.Repositories.Interfaces;

namespace N70.Persistance.Repositories;

public class UserRepository : EntityRepositoryBase<User, IdentityDbContext>,  IUserRepository
{
    public UserRepository(IdentityDbContext dbContext): base(dbContext)
    {
    }

    public ValueTask<User?> GetByIdAsync(Guid userId, bool asNoTracking = false, CancellationToken cancellationToken = default)
    {
        return base.GetByIdAsync(userId, asNoTracking, cancellationToken);
    }

    public ValueTask<User> CrateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return base.CreateAsync(user, saveChanges, cancellationToken);
    }

    public ValueTask<User> UpdateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return base.UpdateAsync(user, saveChanges, cancellationToken);
    }
}