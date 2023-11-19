using Microsoft.EntityFrameworkCore;
using N70.Application.Common.Identity.Services;
using N70.Domain.Entities;
using N70.Domain.Enums;
using N70.Persistance.Repositories.Interfaces;

namespace N70.Infrastructure.Common.Identity.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;

    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }
    public async ValueTask<Role?> GetByTypeAsync(
        RoleType roleType,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default)
    {
        return await _roleRepository.Get(asNoTracking: asNoTracking)
            .SingleOrDefaultAsync(role=>role.Type ==roleType, cancellationToken);
    }
}