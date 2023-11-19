using Microsoft.EntityFrameworkCore;
using N71.Blog.Domain.Entity;

namespace N71.Blog.Persistance.DataContext;

public class BlogsDbContext : DbContext
{
     public DbSet<User> Users => Set<User>();

     DbSet<Role> Roles => Set<Role>();

     DbSet<Blogs> Blog => Set<Blogs>();

     DbSet<Comment> Comments => Set<Comment>();

     public BlogsDbContext(DbContextOptions<BlogsDbContext> options) : base(options){}

     protected override void OnModelCreating(ModelBuilder modelBuilder)
     {
          modelBuilder.ApplyConfigurationsFromAssembly(typeof(BlogsDbContext).Assembly);
     }
}