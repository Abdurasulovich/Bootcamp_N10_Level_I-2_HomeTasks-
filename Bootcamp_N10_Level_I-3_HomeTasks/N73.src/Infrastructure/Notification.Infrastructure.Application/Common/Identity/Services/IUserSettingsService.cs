using Notification.Infrastructure.Domain.Entities;

namespace Notification.Infrastructure.Application.Common.Identity.Services;

public interface IUserSettingsService
{
    ValueTask<UserSettings?> GetByIdAsync(
        Guid userSettingsId,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default);
}
