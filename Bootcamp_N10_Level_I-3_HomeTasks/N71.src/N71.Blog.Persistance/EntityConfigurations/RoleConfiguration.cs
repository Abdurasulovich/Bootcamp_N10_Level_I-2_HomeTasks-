using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using N71.Blog.Domain.Entity;
using N71.Blog.Domain.Enums;

namespace N71.Blog.Persistance.EntityConfigurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasIndex(role => role.Type).IsUnique();
        builder.HasData(new Role
        {
            Id = Guid.Parse("144BBDEE-CC4B-42BC-9FBC-DDE1C286F8B5"),
            Type = RoleType.Admin,
            CratedTime = DateTime.UtcNow
        },
        new Role
        {
            Id = Guid.Parse("CF50DE86-C013-40CD-87B4-55148BF04F93"),
            Type= RoleType.Reader,
            CratedTime = DateTime.UtcNow
        });
        
    }
}