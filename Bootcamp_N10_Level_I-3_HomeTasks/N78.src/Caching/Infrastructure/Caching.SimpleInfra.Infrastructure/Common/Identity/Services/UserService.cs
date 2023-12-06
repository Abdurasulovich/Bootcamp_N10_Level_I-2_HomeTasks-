using Caching.SimpleInfra.Application.Common.Identity.Services;
using Caching.SimpleInfra.Domain.Common.Query;
using Caching.SimpleInfra.Domain.Entities;
using Caching.SimpleInfra.Persistence.Repostiories.Interfaces;
using System.Linq.Expressions;

namespace Caching.SimpleInfra.Infrastructure.Common.Identity.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    public ValueTask<User> CreateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default) =>
        userRepository.CreateAsync(user, saveChanges, cancellationToken);

    public ValueTask<User?> DeleteByIdAsync(Guid userId, bool saveChanges = true, CancellationToken cancellationToken = default)=>
        userRepository.DeleteByIdAsync(userId, saveChanges, cancellationToken);

    public IQueryable<User> Get(Expression<Func<User, bool>>? predicate = null, bool asNoTracking = false)=>
        userRepository.Get(predicate, asNoTracking);

    public async ValueTask<IList<User>> GetAsync(QuerySpecification<User> querySpecification, bool asNoTracking = false, CancellationToken cancellationToken = default)
    {
        return await userRepository.GetAsync(querySpecification, asNoTracking, cancellationToken);
    }

    public ValueTask<User?> GetByIdAsync(Guid userId, bool asNoTracking = false, CancellationToken cancellationToken = default)=>
        userRepository.GetByIdAsync(userId, asNoTracking, cancellationToken);

    public ValueTask<User> UpdateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default)=>
        userRepository.UpdateAsync(user, saveChanges, cancellationToken);
}
