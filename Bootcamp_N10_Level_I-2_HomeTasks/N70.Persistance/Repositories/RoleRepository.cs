using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using N70.Domain.Entities;
using N70.Persistance.DataContexts;
using N70.Persistance.Repositories.Interfaces;

namespace N70.Persistance.Repositories;

public class RoleRepository : EntityRepositoryBase<Role, IdentityDbContext>, IRoleRepository
{
    public RoleRepository(IdentityDbContext dbcontext) : base(dbcontext)
    {
    }

    public new IQueryable<Role> Get(Expression<Func<Role, bool>>? predicate= default, bool asNoTracking = false)
    {
        return base.Get(predicate, asNoTracking);
    }
}