using Notification.Infrastructure.Domain.Common.Entities;
using Notification.Infrastructure.Domain.Enums;

namespace Notification.Infrastructure.Domain.Entities;

public class UserSettings : IEntity
{
    public Guid Id { get; set; }
    
    public NotificationType? PreferredNotificationType { get; set; }
}