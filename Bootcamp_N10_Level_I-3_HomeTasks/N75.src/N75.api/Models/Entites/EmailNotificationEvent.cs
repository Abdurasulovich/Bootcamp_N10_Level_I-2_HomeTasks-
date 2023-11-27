using N75.api.Models.Common;

namespace N75.api.Models.Entites;

public class EmailNotificationEvent : NotificationEvent
{
    public string Subject { get; set; } = default!;

    public string ReceiverEmailAddress { get; set; } = default!;
}
