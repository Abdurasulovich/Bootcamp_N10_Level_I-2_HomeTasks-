using N75.api.Models.Enums;

namespace N75.api.Models.Common;

public abstract class NotificationEvent : Event
{
    public Guid ReceiverUserId { get; set; }

    public string Content { get; set; } = default!;

    public bool IsSuccessful { get; set; }

    public int ResentAttemps { get; set; }

    public NotificationType Type { get; set;}

    public Dictionary<string, string> Variables { get; set; } = new();
}
