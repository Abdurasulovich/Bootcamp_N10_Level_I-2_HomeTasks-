using Microsoft.EntityFrameworkCore;
using N70.Application.Common.Identity.Services;
using N70.Domain.Entities;
using N70.Persistance.Repositories.Interfaces;

namespace N70.Infrastructure.Common.Identity.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository) => _userRepository = userRepository;

    public ValueTask<User> CreateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return _userRepository.CreateAsync(user, saveChanges, cancellationToken);
    }

    public async ValueTask<User?> GetByEmailAddressAsync(
        string emailAddress, 
        bool asNoTracking = false, 
        CancellationToken cancellationToken = default)
    {
        return await _userRepository
            .Get(asNoTracking: asNoTracking)
            .Include(user => user.Role)
            .SingleOrDefaultAsync(user => user.EmailAddress == emailAddress, cancellationToken: cancellationToken);
    }

    public ValueTask<User?> GetByIdAsync(Guid userId, bool asNoTracking = false, CancellationToken cancellationToken = default)
    {
        return _userRepository.GetByIdAsync(userId, asNoTracking, cancellationToken);
    }

    public ValueTask<User> UpdateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return _userRepository.UpdateAsync(user, saveChanges, cancellationToken);
    }
}