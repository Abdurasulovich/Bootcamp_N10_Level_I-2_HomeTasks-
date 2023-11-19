using Type = Notification.Infrastructure.Domain.Enums.NotificationType;
namespace Notification.Infrastructure.Domain.Entities;

public class SmsTemplate : NotificationTemplate
{
    public SmsTemplate()
    {
        Type = Type.Sms;
    }
}