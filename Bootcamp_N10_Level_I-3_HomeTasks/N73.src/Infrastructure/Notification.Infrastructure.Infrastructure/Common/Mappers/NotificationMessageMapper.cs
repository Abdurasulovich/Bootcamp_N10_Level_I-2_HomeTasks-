using AutoMapper;
using Notification.Infrastructure.Application.Common.Notifications.Models;

namespace Notification.Infrastructure.Infrastructure.Common.Mappers;

public class NotificationMessageMapper : Profile
{
    public NotificationMessageMapper()
    {
        CreateMap<EmailNotificationRequest, EmailMessage>();
        CreateMap<SmsNotificationRequest, SmsMessage>();
    }
}
