using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using N70.Domain.Entities;

namespace N70.Persistance.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasOne(user => user.Role).WithMany().HasForeignKey(user => user.RoleId);

        builder.HasData(new User
        {
            Id = Guid.Parse("{97634E3B-3515-4AA6-BAFA-4A81AB2311FD}"),
            FirstName = "Java",
            LastName = "Dev",
            Age = 22,
            EmailAddress = "javaengeineer@gmail.com",
            PasswordHash = "ahgkajfasd",
            IsEmailAddressVerified = true,
            RoleId = Guid.Parse("6d3503ab-1a35-47b9-be09-b24ff4fbf6bf")
        });
    }
}