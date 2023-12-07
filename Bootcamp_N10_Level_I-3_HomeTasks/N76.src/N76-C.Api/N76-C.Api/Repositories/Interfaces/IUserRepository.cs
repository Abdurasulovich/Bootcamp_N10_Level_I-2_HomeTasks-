using N76_C.Api.Entities;
using System.Linq.Expressions;

namespace N76_C.Api.Repositories.Interfaces;

public interface IUserRepository
{
    IQueryable<User> Get(Expression<Func<User, bool>>? predicate, bool asNoTracking = false);

    ValueTask<ICollection<User>> GetbyIdsAsync(
        IEnumerable<Guid> ids,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
        );

    ValueTask<User?> GetbyIdAsync(
        Guid id,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
        );

    ValueTask<User> CreateAsync(
        User user,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
        );

    ValueTask<User> UpdateAsync(
        User user,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
        );

    ValueTask<User?> DeleteAsync(
        User user,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
        );

    ValueTask<User?> DeleteByIdAsync(
        Guid id,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
        );
}
