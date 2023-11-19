using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using N71.Blog.Domain.Entity;
using N71.Blog.Persistance.DataContext;

namespace N71.Blog.Persistance.EntityConfigurations;

public class BlogConfiguration : IEntityTypeConfiguration<Blogs>
{
    public void Configure(EntityTypeBuilder<Blogs> builder)
    {
        builder.HasOne(blog => blog.User).WithMany(user => user.Blogs)
            .HasForeignKey(blog => blog.UserId);
    }
}