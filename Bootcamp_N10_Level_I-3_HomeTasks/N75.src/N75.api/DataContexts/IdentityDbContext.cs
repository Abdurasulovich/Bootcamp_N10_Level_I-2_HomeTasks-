using Microsoft.EntityFrameworkCore;
using N75.api.Models.Entites;

namespace N75.api.DataContexts;

public class IdentityDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();

    public DbSet<EmailNotificationEvent> EmailNotificationEvents => Set<EmailNotificationEvent>();

    public IdentityDbContext() : base(new DbContextOptionsBuilder<IdentityDbContext>()
        .UseNpgsql("Host=localhost;Port=5432;Database=CrudExample;Username=postgres;Password=java2001")
        .Options)
    {
    }

    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityDbContext).Assembly);
    }
}
