using N70.Application.Common.Enums;
using N70.Application.Common.Identity.Services;
using N70.Application.Common.Notifocations.Services;
using N70.Domain.Entities;

namespace N70.Infrastructure.Common.Identity.Services;

public class AccountService : IAccountService
{
    public static readonly List<User> _users = new();
    private readonly IVerificationTokenGeneratorService _tokenGeneratorService;
    private readonly IEmailOrchestrationService _emailOrchestrationService;
    private readonly IUserService _userService;

    public AccountService(IVerificationTokenGeneratorService verification,
        IEmailOrchestrationService email,
        IUserService userService)
    {
        _tokenGeneratorService = verification;
        _emailOrchestrationService = email;
        _userService = userService;
    }

    public List<User> Users => _users;

    public ValueTask<bool> VerificeteAsync(string token, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(token))
            throw new ArgumentException("Invalid verification token", nameof(token));

        var verificationTokenResult = _tokenGeneratorService.DecodeToken(token);

        if (!verificationTokenResult.IsValid)
            throw new InvalidOperationException("Invalid verification token");

        var result = verificationTokenResult.Token.Type switch
        {
            VerificationType.EmailAddressVerificetion => MarkEmailAsVerifiedAsync(verificationTokenResult.Token.UserId),
            _ => throw new InvalidOperationException("This method is not intended to accept other types of tokens")
        };
        
        return result;
    }

    public async ValueTask<bool> CreateUserAsync(User user, CancellationToken cancellationToken = default)
    {
        var createdUser = await _userService.CreateAsync(user, true, cancellationToken);

        var verificationToken = _tokenGeneratorService.GenerateToken(VerificationType.EmailAddressVerificetion, createdUser.Id);

        var verificationEmailResult = await _emailOrchestrationService.SendAsync(createdUser.EmailAddress,
            $"Sistemaga hush kelibsiz - {verificationToken}");
        var result = verificationEmailResult;

        return result;
    }

    public ValueTask<bool> MarkEmailAsVerifiedAsync(Guid userId)
    {
        var foundUser = _users.FirstOrDefault(user => user.Id == userId) ?? throw new InvalidOperationException();
        foundUser.IsEmailAddressVerified = true;
        return new(true);
    }
}