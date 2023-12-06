using System.Linq.Expressions;
using N76.Api.Entities;

namespace N76.Api.Repositories.Interfaces;

public interface IUserRepository
{

    IQueryable<User> Get(Expression<Func<User, bool>> predicate, bool asNoTracking = false);

    ValueTask<ICollection<User>> GetByIdsAsync(IEnumerable<Guid> ids, bool asNoTracking = false, CancellationToken cancellationToken = default);

    ValueTask<User> GetByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default);

    ValueTask<User> CreateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<User> UpdateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<User?> DeleteAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<User?> DeleteByIdAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);
}