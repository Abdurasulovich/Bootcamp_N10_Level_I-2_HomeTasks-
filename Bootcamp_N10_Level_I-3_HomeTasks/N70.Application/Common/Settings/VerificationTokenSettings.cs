namespace N70.Application.Common.Notifocations.Settings;

public class VerificationTokenSettings
{
    public string IdentityVerificationTokenPurpose { get; set; } = default!;
    
    public int IdentityVerificetionExpirationDurationInMinutes { get; set; }
}