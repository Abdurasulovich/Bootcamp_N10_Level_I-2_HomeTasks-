using System.Security.Cryptography.X509Certificates;

namespace N71.Blog.Application.Dtos;

public class CommentDto
{
    public Guid Id { get; set; }

    public string Content { get; set; } = default!;
    
    public Guid UserId { get; set; }
    
    public Guid BlogId { get; set; }
    
    public DateTimeOffset Date { get; set; }
}