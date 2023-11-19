using Notification.Infrastructure.Domain.Common.Entities;
using Notification.Infrastructure.Domain.Enums;

namespace Notification.Infrastructure.Domain.Entities;

public abstract class NotificationTemplate : IEntity
{
    public Guid Id { get; set; }

    public NotificationType Type { get; set; }
    
    public NotificationTemplateType TemplateType { get; set; }
    
    public string Content { get; set; } = default!;

    public IList<NotificationHistory> Histories { get; set; } = new List<NotificationHistory>();
}