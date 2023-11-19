using Notification.Infrastructure.Application.Common.Notifications.Models;
using Notification.Infrastructure.Domain.Common.Exceptions;

namespace Notification.Infrastructure.Application.Common.Notifications.Services;

public interface ISmsOrchestrationService
{
    ValueTask<FuncResult<bool>> SendAsync(
        SmsNotificationRequest request,
        CancellationToken cancellationToken = default
    );
}