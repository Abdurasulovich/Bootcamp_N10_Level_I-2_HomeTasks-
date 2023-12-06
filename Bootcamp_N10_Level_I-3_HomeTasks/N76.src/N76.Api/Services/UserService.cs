using N76.Api.Entities;
using N76.Api.Repositories.Interfaces;
using N76.Api.Services.Interfaces;
using System.Linq.Expressions;

namespace N76.Api.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    public ValueTask<User> CreateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return userRepository.CreateAsync(user, saveChanges, cancellationToken);
    }

    public ValueTask<User?> DeleteAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return userRepository.DeleteAsync(user, saveChanges, cancellationToken);
    }

    public ValueTask<User?> DeleteByIdAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return userRepository.DeleteByIdAsync(id, saveChanges, cancellationToken);
    }

    public IQueryable<User> Get(Expression<Func<User, bool>> predicate, bool asNoTracking = false)
    {
        return userRepository.Get(predicate, asNoTracking);
    }

    public ValueTask<User> GetByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default)
    {
        return userRepository.GetByIdAsync(id, asNoTracking, cancellationToken);
    }

    public ValueTask<ICollection<User>> GetByIdsAsync(IEnumerable<Guid> ids, bool asNoTracking = false, CancellationToken cancellationToken = default)
    {
        return userRepository.GetByIdsAsync(ids, asNoTracking, cancellationToken);
    }

    public async ValueTask<User> UpdateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var found = await userRepository.GetByIdAsync(user.Id, saveChanges, cancellationToken)
            ?? throw new InvalidOperationException("user not found for update!");

        found.FirstName = user.FirstName;
        found.LastName = user.LastName;

        return await userRepository.UpdateAsync(found, saveChanges, cancellationToken);
    }
}
