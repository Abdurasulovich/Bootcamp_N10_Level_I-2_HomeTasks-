using N70.Domain.Entities;

namespace N70.Application.Common.Identity.Services;

public interface IAccountService
{
    List<User> Users { get; }

    ValueTask<bool> VerificeteAsync(string token, CancellationToken cancellationToken =default);
    
    ValueTask<bool> CreateUserAsync(User user, CancellationToken cancellationToken = default);
}