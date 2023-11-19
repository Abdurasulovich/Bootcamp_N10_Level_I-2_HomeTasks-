using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using N71.Blog.Application.Identity.Constants;
using N71.Blog.Application.Identity.Models;
using N71.Blog.Application.Identity.Services.Interfaces;

namespace N71.Blog.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService) => _authService = authService;

    [HttpPost("signUp")]
    public async ValueTask<IActionResult> SignUpAsync([FromForm] SignUpDetails signUpDetails,
        CancellationToken cancellationToken) =>
        Ok(await _authService.SignUpAsync(signUpDetails, cancellationToken));

    [HttpPost("signIn")]
    public async ValueTask<IActionResult> SignInAsync([FromForm] SignInDetails signInDetails,
        CancellationToken cancellationToken) =>
        Ok(await _authService.SignInAsync(signInDetails, cancellationToken));

    [Authorize(Roles = "Admin")]
    [HttpPut("users/{userId}/roles/{roleType}")]
    public async ValueTask<IActionResult> GrandRoleAsync([FromRoute] Guid userId, [FromRoute] string roleType,
        CancellationToken cancellationToken)
    {
        var actionUserId = Guid.Parse(User.Claims.First(claim => claim.Type == ClaimConstants.UserId).Value);
        return Ok(await _authService.GrandRole(userId, roleType, actionUserId, cancellationToken));
    }
}