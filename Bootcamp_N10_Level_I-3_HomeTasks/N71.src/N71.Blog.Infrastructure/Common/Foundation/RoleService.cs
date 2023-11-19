using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using N71.Blog.Application.Foundations;
using N71.Blog.Domain.Entity;
using N71.Blog.Domain.Enums;
using N71.Blog.Persistance.Repositories.Interfaces;

namespace N71.Blog.Infrastructure.Common.Foundation;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;

    public RoleService(IRoleRepository roleRepository) => _roleRepository = roleRepository;

    public IQueryable<Role> Get(Expression<Func<Role, bool>>? predicate = default, bool asNoTracking = false)
        => _roleRepository.Get(predicate, asNoTracking);

    public async ValueTask<IList<Role>> GetByIdsAsync(IEnumerable<Guid> rolesId, bool asNoTracking = false,
        CancellationToken cancellationToken = default)
        => await _roleRepository.GetByIdsAsync(rolesId, asNoTracking, cancellationToken);

    public async ValueTask<Role?> GetByIdAsync(Guid id, bool asNoTracking = false,
        CancellationToken cancellationToken = default)
        => await _roleRepository.GetByIdAsync(id, asNoTracking, cancellationToken);

    public ValueTask<Role?> GetByTypeAsync(RoleType roleType, bool asNoTracking = false,
        CancellationToken cancellationToken = default)
        => new(_roleRepository.Get(asNoTracking: asNoTracking)
                   .SingleOrDefault(role => role.Type == roleType)
               ?? throw new ArgumentNullException(nameof(Role)));
}