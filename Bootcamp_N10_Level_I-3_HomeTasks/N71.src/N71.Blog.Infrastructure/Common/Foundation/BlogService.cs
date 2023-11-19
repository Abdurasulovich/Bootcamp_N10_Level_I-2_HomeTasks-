using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Security.Authentication;
using Microsoft.AspNetCore.Hosting;
using N71.Blog.Application.Foundations;
using N71.Blog.Domain.Entity;
using N71.Blog.Persistance.Repositories.Interfaces;

namespace N71.Blog.Infrastructure.Common.Foundation;

public class BlogService : IBlogService
{
    private readonly IBlogRepository _blogRepository;

    public BlogService(IBlogRepository blogRepository) => _blogRepository = blogRepository;

    public IQueryable<Blogs> Get(Expression<Func<Blogs, bool>>? predicate, bool asNoTracking = false,
        CancellationToken cancellationToken = default)
        => _blogRepository.Get(predicate, asNoTracking);

    public ValueTask<Blogs?> GetByIdAsync(Guid id, bool asNoTracking = false,
        CancellationToken cancellationToken = default)
        => _blogRepository.GetByIdAsync(id, asNoTracking, cancellationToken);

    public ValueTask<IList<Blogs>> GetByIdsAsync(IEnumerable<Guid> ids, bool asNoTracking = false,
        CancellationToken cancellationToken = default)
        => _blogRepository.GetByIdsAsync(ids, asNoTracking, cancellationToken);

    public async ValueTask<Blogs> CreateAsync(Blogs blog, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        if (!IsValidBlog(blog))
            throw new ValidationException("This blog is invalid");

        blog.PublishedDate = DateTime.UtcNow;

        return await _blogRepository.CreateAsync(blog, saveChanges, cancellationToken);
    }

    public async ValueTask<Blogs> UpdateAsync(Blogs blog, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        if (!IsValidBlog(blog))
            throw new ValidationException("Invalid blog");
        var foundBlog = await _blogRepository.GetByIdAsync(blog.Id, cancellationToken: cancellationToken)
                        ?? throw new InvalidOperationException("Blog not found");

        if (foundBlog.UserId != blog.UserId)
            throw new AuthenticationException("Forbidden to update for this user");

        foundBlog.Content = blog.Content;
        foundBlog.Title = blog.Title;
        foundBlog.ModifiedDate = DateTime.UtcNow;

        return await _blogRepository.UpdateAsync(foundBlog, saveChanges, cancellationToken);
    }

    public async ValueTask<Blogs> DeleteAsync(Blogs blog, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var foundBlog = await GetByIdAsync(blog.Id, saveChanges, cancellationToken)
                        ?? throw new InvalidOperationException("Blog not found");
        if (foundBlog.UserId != blog.UserId)
            throw new AuthenticationException("Forbidden to delete for this user");

        return await _blogRepository.DeleteAsync(blog, saveChanges, cancellationToken);
    }

    public async ValueTask<Blogs> DeleteByIdAsync(Guid id, Guid userId, bool saveChanges = true, CancellationToken cancellationToke = default)
    {
        var foundBlog = await GetByIdAsync(id, saveChanges, cancellationToke)
                        ?? throw new InvalidOperationException("Blog not found");
        if (foundBlog.UserId != userId)
            throw new AuthenticationException("Forbidden to delete for this user");

        return await _blogRepository.DeleteByIdAsync(id, saveChanges, cancellationToke);
    }


    private static bool IsValidBlog(Blogs blog) =>
        !(string.IsNullOrWhiteSpace(blog.Title)
          || string.IsNullOrWhiteSpace(blog.Content));
}