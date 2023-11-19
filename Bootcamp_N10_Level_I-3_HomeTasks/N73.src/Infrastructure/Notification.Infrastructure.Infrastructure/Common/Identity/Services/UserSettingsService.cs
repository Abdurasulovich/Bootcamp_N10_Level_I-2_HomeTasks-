using Notification.Infrastructure.Application.Common.Identity.Services;
using Notification.Infrastructure.Domain.Entities;
using Notification.Infrastructure.Persistance.Repositories.Interfaces;

namespace Notification.Infrastructure.Infrastructure.Common.Identity.Services;

public class UserSettingsService : IUserSettingsService
{
    private readonly IUserSettingsRepository _userSettingsRepository;

    public UserSettingsService(IUserSettingsRepository userSettings)
    {
        _userSettingsRepository = userSettings;
    }

    public ValueTask<UserSettings?> GetByIdAsync(Guid userSettingsId, bool asNoTracking = false,
        CancellationToken cancellationToken = default)
        => _userSettingsRepository.GetByIdAsync(userSettingsId, asNoTracking, cancellationToken);
}