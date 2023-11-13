using N70.Application.Common.Enums;
using N70.Application.Identity.Models;

namespace N70.Application.Common.Identity.Services;

public interface IVerificationTokenGeneratorService
{
    string GenerateToken(VerificationType type, Guid userId);

    (VerificationToken Token, bool IsValid) DecodeToken(string token);
}