using Microsoft.AspNetCore.Mvc;
using N70.Application.Common.Constants;
using N70.Application.Common.Identity.Services;
using N70.Application.Identity.Models;

namespace N70.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async ValueTask<IActionResult> Register([FromForm] RegistrationDetails resiter)
    {
        var result = await _authService.RegisterAsync(resiter);
        return Ok(result);
    }

    [HttpPost("login")]
    public async ValueTask<IActionResult> Login([FromBody] LoginDetails login)
    {
        var result = await _authService.LoginAsync(login);

        return Ok(result);
    }

    [HttpPut("users/{userId:guid}/roles/{roleType}")]
    public async ValueTask<IActionResult> GrandRole([FromRoute] Guid userId, [FromRoute] string roleType)
    {
        var actionUserId = Guid.Parse(User.Claims.First(claim => claim.Type.Equals(ClaimConstants.UserId)).Value);
        var result = await _authService.GrandRoleAsync(userId, roleType, actionUserId);
        return result ? Ok() : BadRequest();
    }
}