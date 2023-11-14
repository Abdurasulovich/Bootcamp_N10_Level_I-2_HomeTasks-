using System.Linq.Expressions;
using N71.Blog.Domain.Entity;

namespace N71.Blog.Application.Foundations;

public interface IBlogService
{
    IQueryable<Blogs> Get(Expression<Func<Blogs, bool>>? predicate, bool asNoTracking = false,
        CancellationToken cancellationToken = default);

    ValueTask<Blogs?> GetById(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default);

    ValueTask<IList<Blogs>> GetByIdsAsync(IEnumerable<Guid> ids, bool asNoTracking = false,
        CancellationToken cancellationToken = default);

    ValueTask<Blogs> CreateAsync(Blogs blog, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<Blogs> UpdateAsync(Blogs blog, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<Blogs> DeleteAsync(Blogs blog, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<Blogs> DeleteByIdAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToke = default);
}