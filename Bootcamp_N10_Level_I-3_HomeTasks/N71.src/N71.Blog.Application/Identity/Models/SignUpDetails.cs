using Microsoft.VisualBasic.CompilerServices;

namespace N71.Blog.Application.Identity.Models;

public class SignUpDetails
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string EmailAddress { get; set; } = default!;
    public string Password { get; set; } = default!;
}