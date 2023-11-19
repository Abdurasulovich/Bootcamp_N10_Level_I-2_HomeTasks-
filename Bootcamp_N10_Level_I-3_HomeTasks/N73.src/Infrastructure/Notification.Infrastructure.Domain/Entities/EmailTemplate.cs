using Type = Notification.Infrastructure.Domain.Enums.NotificationType;
namespace Notification.Infrastructure.Domain.Entities;

public class EmailTemplate : NotificationTemplate
{
    public EmailTemplate()
    {
        Type = Type.Email;
    }

    public string Subject { get; set; } = default!;
}