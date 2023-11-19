using Microsoft.EntityFrameworkCore;
using N71.Blog.Domain.Entity;
using N71.Blog.Persistance.DataContext;
using N71.Blog.Persistance.Repositories.Interfaces;

namespace N71.Blog.Persistance.Repositories.Services;

public class CommentRepository : EntityRepositoryBase<Comment, BlogsDbContext>, ICommentRepository
{
    public CommentRepository(BlogsDbContext dbContext) : base(dbContext)
    {
    }
}