using System.Linq.Expressions;
using N70.Domain.Entities;

namespace N70.Persistance.Repositories.Interfaces;

public interface IRoleRepository
{
    IQueryable<Role> Get(Expression<Func<Role, bool>>? predicate = default, bool asNoTracking = false);
}