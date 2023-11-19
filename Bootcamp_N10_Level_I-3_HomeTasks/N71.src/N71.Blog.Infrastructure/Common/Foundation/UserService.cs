using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Security.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using N71.Blog.Application.Foundations;
using N71.Blog.Domain.Entity;
using N71.Blog.Infrastructure.Common.Identity.Serivces;
using N71.Blog.Persistance.Repositories.Interfaces;

namespace N71.Blog.Infrastructure.Common.Foundation;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository) => _userRepository = userRepository;

    public IQueryable<User> Get(Expression<Func<User, bool>>? predicate, bool asNoTracking = false)
        => _userRepository.Get(predicate, asNoTracking);

    public async ValueTask<User?> GetByIdAsync(Guid userId, bool asNoTracking = false,
        CancellationToken cancellationToken = default)
        => await _userRepository.GetByIdAsync(userId, asNoTracking, cancellationToken);

    public async ValueTask<IList<User>> GetByIdsAsync(IEnumerable<Guid> usersId, bool asNoTracking = false,
        CancellationToken cancellationToken = default)
        => await _userRepository.GetByIdsAsync(usersId, asNoTracking, cancellationToken);

    public async ValueTask<User> CreateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        if (!IsValidUser(user))
            throw new ValidationException(nameof(user));

        return await _userRepository.CreateAsync(user, saveChanges, cancellationToken);
    }

    public async ValueTask<User> UpdateAsync(User user, bool saveChages = true, CancellationToken cancellationToken = default)
    {
        var foundUser = await _userRepository.GetByIdAsync(user.Id, cancellationToken: cancellationToken)
                        ?? throw new InvalidOperationException("User not found");
        
        foundUser.FirstName = user.FirstName;
        foundUser.LastName = user.LastName;
        foundUser.Age = user.Age;
        foundUser.EmailAddress = user.EmailAddress;
        foundUser.IsEmailAddressVerified = true;
        foundUser.PasswordHash = user.PasswordHash;

        return await _userRepository.UpdateAsync(foundUser, saveChages, cancellationToken);
    }

    public async ValueTask<User> DeleteAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var foundUser = await _userRepository.GetByIdAsync(user.Id, cancellationToken: cancellationToken)
                        ?? throw new InvalidOperationException("User not found");
        if (foundUser.Id != user.Id)
            throw new AuthenticationException("Forbidden to delete user for this user");
        return await _userRepository.DeleteAsync(user, saveChanges, cancellationToken);
    }

    public async ValueTask<User> DeleteByIdAsync(Guid userId, bool saveChanges = true,
        CancellationToken cancellationToken = default)
        => await _userRepository.DeleteByIdAsync(userId, saveChanges, cancellationToken);
    private static bool IsValidUser(User user) =>
        !(string.IsNullOrWhiteSpace(user.FirstName)
          || string.IsNullOrWhiteSpace(user.LastName)
          || string.IsNullOrWhiteSpace(user.EmailAddress)
          || string.IsNullOrWhiteSpace(user.PasswordHash));
}