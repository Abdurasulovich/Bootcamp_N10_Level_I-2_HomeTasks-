using Microsoft.EntityFrameworkCore;
using N71.Blog.Domain.Entity;
using N71.Blog.Persistance.DataContext;
using N71.Blog.Persistance.Repositories.Interfaces;

namespace N71.Blog.Persistance.Repositories.Services;

public class BlogRepository : EntityRepositoryBase<Blogs, BlogsDbContext>, IBlogRepository
{

    public BlogRepository(BlogsDbContext dbContext) : base(dbContext)
    {
    }
}