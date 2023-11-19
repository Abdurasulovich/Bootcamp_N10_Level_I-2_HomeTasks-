using Microsoft.EntityFrameworkCore;
using N71.Blog.Application.Foundations;
using N71.Blog.Application.Identity.Services.Interfaces;
using N71.Blog.Domain.Entity;

namespace N71.Blog.Infrastructure.Common.Identity.Serivces;

public class AccountService : IAccountService
{
    private readonly IUserService _userService;

    public AccountService(IUserService userService) => _userService = userService;

    public async ValueTask<User?> GetUserByEmailAddress(string emailAddress)
        => await _userService.Get(self => true, asNoTracking: true)
            .Include(user => user.Role)
            .SingleOrDefaultAsync(user => user.EmailAddress == emailAddress);

    public async ValueTask<bool> CreateUserAsync(User user, CancellationToken cancellationToken = default)
    {
        await _userService.CreateAsync(user, cancellationToken: cancellationToken);
        return true;
    }
}