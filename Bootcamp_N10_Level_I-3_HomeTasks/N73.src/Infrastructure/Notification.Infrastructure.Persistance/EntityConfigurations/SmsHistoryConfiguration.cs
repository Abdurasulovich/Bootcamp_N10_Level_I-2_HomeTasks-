using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notification.Infrastructure.Domain.Entities;

namespace Notification.Infrastructure.Persistance.EntityConfigurations;

public class SmsHistoryConfiguration : IEntityTypeConfiguration<SmsHistory>
{
    public void Configure(EntityTypeBuilder<SmsHistory> builder)
    {
        builder.Property(template => template.SenderPhoneNumber).IsRequired().HasMaxLength(32);
        builder.Property(template => template.ReceiverPhoneNumber).IsRequired().HasMaxLength(32);
    }
}