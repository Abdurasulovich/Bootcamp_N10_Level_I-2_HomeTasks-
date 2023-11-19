using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using N71.Blog.Domain.Entity;

namespace N71.Blog.Application.ManagementServices.Interfaces;

public interface IBlogManagementService
{
    ValueTask<IList<Blogs>> GetBlogsByUserId(Guid userId, CancellationToken cancellationToken = default);
    ValueTask<Comment> CreateCommentAsync(Comment comment, CancellationToken cancellationToken = default);
    ValueTask<IList<Comment>> GetCommentsByBlogsIdAsync(Guid blogId, CancellationToken cancellationToken = default);
    ValueTask<IList<User>> GetPopularBloggersAsync(CancellationToken cancellationToken = default);
}