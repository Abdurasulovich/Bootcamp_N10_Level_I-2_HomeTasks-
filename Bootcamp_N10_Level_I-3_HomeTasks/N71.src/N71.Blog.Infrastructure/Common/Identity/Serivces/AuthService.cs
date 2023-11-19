using System.Security.Authentication;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using N71.Blog.Application.Foundations;
using N71.Blog.Application.Identity.Models;
using N71.Blog.Application.Identity.Services.Interfaces;
using N71.Blog.Domain.Entity;
using N71.Blog.Domain.Enums;

namespace N71.Blog.Infrastructure.Common.Identity.Serivces;

public class AuthService : IAuthService
{
    private readonly IAccessTokenGeneratorService _accessTokenGeneratorService;
    private readonly IAccountService _accountService;
    private readonly IPasswordHasherService _passwordHasher;
    private readonly IMapper _mapper;
    private readonly IRoleService _roleService;
    private readonly IUserService _userService;

    public AuthService(
        IAccessTokenGeneratorService accessTokenGeneratorService,
        IAccountService accountService,
        IPasswordHasherService passwordHasher,
        IMapper mapper,
        IRoleService roleService,
        IUserService userService)
    {
        _accessTokenGeneratorService = accessTokenGeneratorService;
        _accountService = accountService;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
        _roleService = roleService;
        _userService = userService;
    }
    public async ValueTask<bool> SignUpAsync(SignUpDetails signUpDetails, CancellationToken cancellationToken = default)
    {
        var foundUser = await _accountService.GetUserByEmailAddress(signUpDetails.EmailAddress);
        if (foundUser is not null)
            throw new InvalidOperationException("User with this email address already exists");

        var user = _mapper.Map<User>(signUpDetails);

        user.PasswordHash = _passwordHasher.HashPassword(signUpDetails.Password);
        var role = await _roleService.GetByTypeAsync(RoleType.Reader, true, cancellationToken)
                   ?? throw new InvalidOperationException("This role type does not exists");
        user.RoleId = role.Id;

        return await _accountService.CreateUserAsync(user, cancellationToken: cancellationToken);
    }

    public async ValueTask<string> SignInAsync(SignInDetails signInDetails, CancellationToken cancellationToken = default)
    {
        var foundUser = await _accountService.GetUserByEmailAddress(signInDetails.EmailAddress) ??
                        throw new InvalidOperationException(nameof(SignInDetails));

        if (!_passwordHasher.VerifyPassword(signInDetails.Password, foundUser.PasswordHash))
            throw new InvalidOperationException();

        var token = _accessTokenGeneratorService.GetToken(foundUser);

        return token;
    }

    public async ValueTask<bool> GrandRole(Guid userId, string roleType, Guid actionUserId, CancellationToken cancellationToken = default)
    {
        var user = await _userService.GetByIdAsync(userId, cancellationToken: cancellationToken)
                   ?? throw new ArgumentNullException(nameof(userId));

        _ = await _userService.GetByIdAsync(actionUserId, cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException();

        if (!Enum.TryParse(roleType, out RoleType type))
            throw new InvalidOperationException();

        var role = await _roleService.GetByTypeAsync(type, true, cancellationToken)
                   ?? throw new InvalidOperationException();
        user.RoleId = role.Id;
        await _userService.UpdateAsync(user, true, cancellationToken);
        return true;
    }
}