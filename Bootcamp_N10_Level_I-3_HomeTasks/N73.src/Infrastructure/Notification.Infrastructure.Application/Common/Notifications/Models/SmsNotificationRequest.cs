using Notification.Infrastructure.Domain.Enums;

namespace Notification.Infrastructure.Application.Common.Notifications.Models;

public class SmsNotificationRequest : NotificationRequest
{
    public SmsNotificationRequest() => Type = NotificationType.Sms;
}
