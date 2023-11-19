using N70.Application.Common.Enums;

namespace N70.Application.Identity.Models;

public class VerificationToken
{
    public Guid UserId { get; set; }
    
    public VerificationType Type { get; set; }
    
    public DateTimeOffset ExpiryTime { get; set; }
}