using Microsoft.EntityFrameworkCore;
using N71.Blog.Domain.Entity;
using N71.Blog.Persistance.DataContext;
using N71.Blog.Persistance.Repositories.Interfaces;

namespace N71.Blog.Persistance.Repositories.Services;

public class UserRepository : EntityRepositoryBase<User, BlogsDbContext>, IUserRepository
{
    public UserRepository(BlogsDbContext dbContext) : base(dbContext)
    {
    }
}