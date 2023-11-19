using System.Linq.Expressions;
using N71.Blog.Domain.Entity;

namespace N71.Blog.Persistance.Repositories.Interfaces;

public interface ICommentRepository
{
    IQueryable<Comment> Get(Expression<Func<Comment, bool>>? predicate = default, bool asNoTracking = false);

    ValueTask<IList<Comment>> GetByIdsAsync(IEnumerable<Guid> ids, bool asNoTracking = false,
        CancellationToken cancellationToken = default);

    ValueTask<Comment?> GetByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default);

    ValueTask<Comment> CreateAsync(Comment comment, bool saveChanges = true,
        CancellationToken cancellationToken = default);

    ValueTask<Comment> UpdateAsync(Comment comment, bool saveChanges = true,
        CancellationToken cancellationToken = default);

    ValueTask<Comment> DeleteAsync(Comment comment, bool saveChanges = true,
        CancellationToken cancellationToken = default);

    ValueTask<Comment> DeleteByIdAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);
}