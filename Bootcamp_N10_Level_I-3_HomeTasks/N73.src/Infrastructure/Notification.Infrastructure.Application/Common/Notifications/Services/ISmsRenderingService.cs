using Notification.Infrastructure.Application.Common.Notifications.Models;

namespace Notification.Infrastructure.Application.Common.Notifications.Services;

public interface ISmsRenderingService
{
    ValueTask<string> RenderAsync(
        SmsMessage smsMessage,
        CancellationToken cancellationToken = default
    );
}