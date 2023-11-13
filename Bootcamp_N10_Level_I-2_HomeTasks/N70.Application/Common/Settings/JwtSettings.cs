namespace N70.Application.Common.Notifocations.Settings;

public class JwtSettings
{
    public bool ValidateIssure { get; set; }

    public string ValidIssure { get; set; } = default!;
    
    public bool ValidateAudence { get; set; }

    public string ValidAudence { get; set; } = default!;
    
    public bool ValidateLifeTime { get; set; } 
    
    public int ExpirationTimeInMinutes { get; set; }
    
    public bool ValidateIssureSigningKey { get; set; }

    public string SecretKey { get; set; } = default!;
}