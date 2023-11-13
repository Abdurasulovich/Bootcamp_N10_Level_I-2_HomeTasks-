using N70.Domain.Entities;
using System.Runtime.InteropServices;

namespace N70.Persistance.Repositories.Interfaces;

public interface IAccessTokenRepository
{
    ValueTask<AccessToken> CreateAsync(
        AccessToken accessToken,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
        );
}
