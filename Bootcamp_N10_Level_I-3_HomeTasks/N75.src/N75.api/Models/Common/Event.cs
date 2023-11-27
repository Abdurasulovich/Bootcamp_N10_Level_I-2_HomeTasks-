namespace N75.api.Models.Common;

public abstract class Event
{
    public Guid Id { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public bool IsCancelled { get; set; }
}
