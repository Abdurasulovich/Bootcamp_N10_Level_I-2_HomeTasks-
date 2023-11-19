using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using N71.Blog.Domain.Entity;

namespace N71.Blog.Persistance.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasOne(user => user.Role).WithMany().HasForeignKey(user => user.RoleId);

        builder.HasData(new User
        {
            Id = Guid.NewGuid(),
            FirstName = "Java",
            LastName = "Dev",
            Age = 22,
            EmailAddress = "javaengeineer@gmail.com",
            PasswordHash = "Djalekeev451",
            IsEmailAddressVerified = true,
            RoleId = Guid.Parse("144BBDEE-CC4B-42BC-9FBC-DDE1C286F8B5")
        });
    }
}