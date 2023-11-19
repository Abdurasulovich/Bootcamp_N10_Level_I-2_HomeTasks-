using Sms.Infrastructure.Application.Notifications.Models;
using Sms.Infrastructure.Domain.Common.Exceptions;

namespace Sms.Infrastructure.Application.Notifications.Services;

public interface INotificationAggregatorService
{
    ValueTask<FuncResult<bool>> SendAsync(
        NotificationRequest notificationRequest,
        CancellationToken cancellationToken = default
    );
}