using Notification.Infrastructure.Application.Common.Notifications.Models;
using Notification.Infrastructure.Domain.Common.Exceptions;

namespace Notification.Infrastructure.Application.Common.Notifications.Services;

public interface IEmailOrchestrationService
{
    ValueTask<FuncResult<bool>> SendAsync(
        EmailNotificationRequest request,
        CancellationToken cancellationToken = default
    );
}