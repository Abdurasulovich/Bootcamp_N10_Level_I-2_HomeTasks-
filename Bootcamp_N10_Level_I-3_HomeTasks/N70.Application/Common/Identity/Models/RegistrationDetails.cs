namespace N70.Application.Identity.Models;

public class RegistrationDetails
{
    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;
    
    public int Age { get; set; }

    public string EmailAddress { get; set; } = string.Empty;

    public string Password { get; set; } = default!;
}