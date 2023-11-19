using System.Linq.Expressions;
using N71.Blog.Domain.Entity;

namespace N71.Blog.Application.Foundations;

public interface IUserService
{
    IQueryable<User> Get(Expression<Func<User, bool>>? predicate, bool asNoTracking = false);

    ValueTask<User?> GetByIdAsync(Guid userId, bool asNoTracking = false, CancellationToken cancellationToken = default);

    ValueTask<IList<User>> GetByIdsAsync(IEnumerable<Guid> usersId, bool asNoTracking = false,
        CancellationToken cancellationToken = default);
    ValueTask<User> CreateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<User> UpdateAsync(User user, bool saveChages = true, CancellationToken cancellationToken = default);

    ValueTask<User> DeleteAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<User> DeleteByIdAsync(Guid userId, bool saveChanges = true,
        CancellationToken cancellationToken = default);
}