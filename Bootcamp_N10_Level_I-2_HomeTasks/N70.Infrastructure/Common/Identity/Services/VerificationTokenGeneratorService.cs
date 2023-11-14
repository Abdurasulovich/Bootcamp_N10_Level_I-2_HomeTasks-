using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;
using N70.Application.Common.Enums;
using N70.Application.Common.Identity.Services;
using N70.Application.Common.Notifocations.Settings;
using N70.Application.Identity.Models;
using Newtonsoft.Json;

namespace N70.Infrastructure.Common.Identity.Services;

public class VerificationTokenGeneratorService : IVerificationTokenGeneratorService
{
    private readonly IDataProtector _dataProtector;
    private readonly VerificationTokenSettings _verificationTokenSettings;

    public VerificationTokenGeneratorService(IDataProtectionProvider protectionProvider,
        IOptions<VerificationTokenSettings> tokenSettings)
    {
        _verificationTokenSettings = tokenSettings.Value;
        _dataProtector =
            protectionProvider.CreateProtector(_verificationTokenSettings.IdentityVerificationTokenPurpose);
        
    }
    public string GenerateToken(VerificationType type, Guid userId)
    {
        var verificationToken = new VerificationToken
        {
            UserId = userId,
            Type = type,
            ExpiryTime =
                DateTimeOffset.UtcNow.AddMinutes(_verificationTokenSettings
                    .IdentityVerificetionExpirationDurationInMinutes)
        };

        return _dataProtector.Protect(JsonConvert.SerializeObject(verificationToken));
    }

    public (VerificationToken Token, bool IsValid) DecodeToken(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
            throw new ArgumentNullException(nameof(token));

        var unprotectedToken = _dataProtector.Unprotect(token);
        var verificationToken = JsonConvert.DeserializeObject<VerificationToken>(unprotectedToken) ??
                                throw new ArgumentException("Invalid verification model", nameof(token));

        return (verificationToken, verificationToken.ExpiryTime > DateTimeOffset.UtcNow);
    }
}