using N71.Blog.Domain.Common;

namespace N71.Blog.Domain.Entity;

public class Comment : IEntity
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    
    public Guid BlogId { get; set; }
    
    public string Content { get; set; } = default!;

    public DateTime CreatedTime { get; set; }

    public DateTimeOffset? ModifiedTime { get; set; }

    public virtual Blogs Blogs { get; set; } = default!;
}