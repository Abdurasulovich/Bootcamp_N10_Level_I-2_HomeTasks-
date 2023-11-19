using N71.Blog.Domain.Common;

namespace N71.Blog.Domain.Entity;

public class User : IEntity
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;
    
    public int Age { get; set; }

    public string EmailAddress { get; set; } = default!;

    public string PasswordHash { get; set; } = default!;
    
    public bool IsEmailAddressVerified { get; set; }
    
    public Guid RoleId { get; set; }

    public virtual Role Role { get; set; } = default!;

    public virtual ICollection<Blogs> Blogs { get; set; } = default!;

    public virtual ICollection<Comment> Comments { get; set; } = default!;
}