using N71.Blog.Application.Identity.Models;

namespace N71.Blog.Application.Identity.Services.Interfaces;

public interface IAuthService
{
    ValueTask<bool> SignUpAsync(SignUpDetails signUpDetails, CancellationToken cancellationToken = default);
    ValueTask<string> SignInAsync(SignInDetails signInDetails, CancellationToken cancellationToken = default);

    ValueTask<bool> GrandRole(Guid userId, string roleType, Guid actionUserId,
        CancellationToken cancellationToken = default);
}