using System.Linq.Expressions;
using N71.Blog.Domain.Entity;

namespace N71.Blog.Persistance.Repositories.Interfaces;

public interface IBlogRepository
{
    public IQueryable<Blogs> Get(Expression<Func<Blogs, bool>>? predicate = default, bool asNoTracking = false);

    public ValueTask<IList<Blogs>> GetByIdsAsync(IEnumerable<Guid> ids, bool asNoTacking = false,
        CancellationToken cancellationToken = default);

    public ValueTask<Blogs?> GetByIdAsync(Guid id, bool asNoTracking = false,
        CancellationToken cancellationToken = default);

    public ValueTask<Blogs> CreateAsync(Blogs blog, bool saveChanges = true, CancellationToken cancellationToken = default);

    public ValueTask<Blogs> UpdateAsync(Blogs blog, bool saveChanges = true, CancellationToken cancellationToken = default);
    public ValueTask<Blogs> DeleteAsync(Blogs blog, bool saveChanges = true, CancellationToken cancellationToken = default);
    public ValueTask<Blogs> DeleteByIdAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);
}