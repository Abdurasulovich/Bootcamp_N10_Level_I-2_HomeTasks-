using N71.Blog.Domain.Entity;

namespace N71.Blog.Application.Identity.Services.Interfaces;

public interface IAccountService
{
    ValueTask<User?> GetUserByEmailAddress(string emailAddress);
    ValueTask<bool> CreateUserAsync(User user, CancellationToken cancellationToken = default);
}