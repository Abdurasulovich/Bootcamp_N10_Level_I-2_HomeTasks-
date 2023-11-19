using Notification.Infrastructure.Domain.Enums;

namespace Notification.Infrastructure.Application.Common.Models.Querying;

public class NotificationTemplateFilter : FilterPagination
{
    public IList<NotificationType> TemplateType { get; set; }

}
