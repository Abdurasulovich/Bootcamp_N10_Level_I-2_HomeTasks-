using System.ComponentModel.Design;
using N71.Blog.Domain.Common;

namespace N71.Blog.Domain.Entity;

public class Blogs : IEntity
{
    public Guid Id { get; set; }

    public string Title { get; set; } = default!;

    public string Content { get; set; } = default!;
    
    public DateTime PublishedDate { get; set; }
    
    public DateTime ModifiedDate { get; set; }
    
    public Guid UserId { get; set; }

    public virtual User User { get; set; } = default!;

    public virtual ICollection<Comment> Comments { get; set; } = default!;
}