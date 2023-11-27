namespace N75.api.Models.Entites;

public class User
{
    public Guid Id { get; set; }

    public string EmailAddress { get; set; } = default!;
}
