using Microsoft.EntityFrameworkCore;
using N76.Api.Entities;

namespace N76.Api.DbContexts;

public class IdentityDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();    

    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
    {
    }
}
