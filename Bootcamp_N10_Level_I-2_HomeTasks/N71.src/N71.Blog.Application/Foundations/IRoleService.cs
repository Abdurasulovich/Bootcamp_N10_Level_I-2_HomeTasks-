using System.Linq.Expressions;
using N71.Blog.Domain.Entity;
using N71.Blog.Domain.Enums;

namespace N71.Blog.Application.Foundations;

public interface IRoleService
{
    IQueryable<Role> Get(Expression<Func<Role, bool>>? predicate = default, bool asNoTracking = false);

    ValueTask<IList<Role>> GetByIdsAsync(IEnumerable<Guid> rolesId, bool asNoTracking = false, CancellationToken cancellationToken = default);

    ValueTask<Role?> GetByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default);

    ValueTask<Role?> GetByTypeAsync(RoleType roleType, bool asNoTracking = false,
        CancellationToken cancellationToken = default);
    
}