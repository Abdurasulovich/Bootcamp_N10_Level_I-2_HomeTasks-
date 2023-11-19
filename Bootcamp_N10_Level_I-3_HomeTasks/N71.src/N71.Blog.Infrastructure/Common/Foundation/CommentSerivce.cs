using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Security.Authentication;
using N71.Blog.Application.Foundations;
using N71.Blog.Domain.Entity;
using N71.Blog.Persistance.Repositories.Interfaces;

namespace N71.Blog.Infrastructure.Common.Foundation;

public class CommentSerivce : ICommentService
{
    private readonly ICommentRepository _commentRepository;

    public CommentSerivce(ICommentRepository commentRepository) => _commentRepository = commentRepository;

    public IQueryable<Comment> Get(Expression<Func<Comment, bool>>? predicate, bool asNoTracking = false)
        => _commentRepository.Get(predicate, asNoTracking);

    public async ValueTask<Comment?> GetByIdAsync(Guid id, bool asNoTracking = false,
        CancellationToken cancellationToken = default)
        => await _commentRepository.GetByIdAsync(id, asNoTracking, cancellationToken);

    public async ValueTask<IList<Comment>> GetByIdsAsync(IEnumerable<Guid> ids, bool asNoTracking = false,
        CancellationToken cancellationToken = default)
        => await _commentRepository.GetByIdsAsync(ids, asNoTracking, cancellationToken);

    public async ValueTask<Comment> CreateAsync(Comment comment, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        if (!IsValidComment(comment))
            throw new ValidationException(nameof(comment));

        comment.CreatedTime = DateTime.UtcNow;

        return await _commentRepository.CreateAsync(comment, saveChanges, cancellationToken);
    }

    public async ValueTask<Comment> UpdateAsync(Comment comment, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        if (!IsValidComment(comment))
            throw new ValidationException(nameof(comment));
        var foundComment = await _commentRepository.GetByIdAsync(comment.Id, cancellationToken: cancellationToken)
                           ?? throw new InvalidOperationException("Comment not found");
        if (foundComment.UserId != comment.UserId)
            throw new AuthenticationException("Forbidden to change comment for this user");

        foundComment.Content = comment.Content;
        foundComment.ModifiedTime = DateTime.UtcNow;

        return await _commentRepository.UpdateAsync(foundComment, saveChanges, cancellationToken);
    }

    public async ValueTask<Comment> DeleteAsync(Comment comment, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var foundComment = await GetByIdAsync(comment.Id, saveChanges, cancellationToken) ??
                         throw new InvalidOperationException("Comment not found");
        if (foundComment.UserId != comment.UserId)
            throw new AuthenticationException("Forbidden to delete for this user");
        return await _commentRepository.DeleteAsync(comment, saveChanges, cancellationToken);
    }

    public async ValueTask<Comment> DeleteByIdAsync(Guid id, Guid userId, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var foundComment = await _commentRepository.GetByIdAsync(id, cancellationToken: cancellationToken) ??
                           throw new InvalidOperationException("Comment not found");
        if (foundComment.UserId != userId)
            throw new AuthenticationException("Forbidden to delete for this user");
        
        return await _commentRepository.DeleteByIdAsync(id, saveChanges, cancellationToken);
    }

    private static bool IsValidComment(Comment comment)
        => !string.IsNullOrWhiteSpace(comment.Content);
}