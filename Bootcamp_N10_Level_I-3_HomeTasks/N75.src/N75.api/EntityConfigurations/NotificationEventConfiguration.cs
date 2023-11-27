using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using N75.api.Models.Common;
using N75.api.Models.Entites;
using N75.api.Models.Enums;

namespace N75.api.EntityConfigurations;

public class NotificationEventConfiguration : IEntityTypeConfiguration<NotificationEvent>
{
    public void Configure(EntityTypeBuilder<NotificationEvent> builder)
    {
        builder
            .ToTable("NotificationEvents")
            .HasDiscriminator(notificationEvent => notificationEvent.Type)
            .HasValue<EmailNotificationEvent>(NotificationType.Email);
    }

}
