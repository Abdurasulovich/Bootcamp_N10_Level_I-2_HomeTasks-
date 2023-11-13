using N70.Domain.Entities;
using N70.Domain.Enums;

namespace N70.Application.Common.Identity.Services;

public interface IRoleService
{
    ValueTask<Role?> GetByTypeAsync(RoleType roleType, bool asNoTracking = false, CancellationToken cancellationToken = default);
}