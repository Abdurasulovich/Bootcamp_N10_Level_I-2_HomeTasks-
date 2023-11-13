using N70.Domain.Entities;
using N70.Persistance.DataContexts;
using N70.Persistance.Repositories.Interfaces;

namespace N70.Persistance.Repositories;

public class AccessTokenRepository : EntityRepositoryBase<AccessToken, IdentityDbContext>, IAccessTokenRepository
{
    public AccessTokenRepository(IdentityDbContext dbContext) : base(dbContext)
    {
        
    }

    public ValueTask<AccessToken> CreateAsync(AccessToken accessToken, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return base.CreateAsync(accessToken, saveChanges, cancellationToken);
    }
}
