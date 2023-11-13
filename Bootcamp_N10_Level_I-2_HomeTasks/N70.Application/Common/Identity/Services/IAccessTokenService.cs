using N70.Domain.Entities;

namespace N70.Application.Common.Identity.Services;

public interface IAccessTokenService
{
    ValueTask<AccessToken> CreateAsync(
        Guid userId,
        string value,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
        );
}
