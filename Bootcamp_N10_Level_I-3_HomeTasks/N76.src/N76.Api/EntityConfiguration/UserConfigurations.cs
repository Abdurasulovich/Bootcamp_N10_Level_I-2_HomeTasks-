using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using N76.Api.Entities;

namespace N76.Api.EntityConfiguration;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(user => user.FirstName).IsRequired().HasMaxLength(126);

        builder.Property(user => user.LastName).IsRequired().HasMaxLength(126);
    }
}
