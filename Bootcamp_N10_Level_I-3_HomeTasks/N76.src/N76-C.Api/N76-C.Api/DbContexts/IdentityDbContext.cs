using Microsoft.EntityFrameworkCore;
using N76_C.Api.Entities;

namespace N76_C.Api.DbContexts;

public class IdentityDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();

    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
    {
        
    }
}
