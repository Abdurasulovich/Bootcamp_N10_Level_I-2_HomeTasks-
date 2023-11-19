using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notification.Infrastructure.Domain.Entities;

namespace Notification.Infrastructure.Persistance.EntityConfigurations;

public class NotificationHistoryTemplate : IEntityTypeConfiguration<NotificationHistory>
{
    public void Configure(EntityTypeBuilder<NotificationHistory> builder)
    {
        throw new NotImplementedException();
    }
}