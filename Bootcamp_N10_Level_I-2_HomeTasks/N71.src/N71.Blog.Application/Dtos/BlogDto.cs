namespace N71.Blog.Application.Dtos;

public class BlogDto
{
    public Guid Id { get; set; }

    public string Title { get; set; } = default!;

    public string Content { get; set; } = default!;
    
    public DateTime PublishedDate { get; set; }
}