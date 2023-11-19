using Notification.Infrastructure.Application.Common.Notifications.Models;

namespace Notification.Infrastructure.Application.Common.Notifications.Services;

public interface IEmailSenderService
{
    ValueTask<bool> SendAsync(EmailMessage emailMessage, CancellationToken cancellationToken = default);
}