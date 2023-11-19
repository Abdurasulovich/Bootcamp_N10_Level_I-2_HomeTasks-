using Microsoft.AspNetCore.Mvc;
using N70.Application.Common.Identity.Services;

namespace N70.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPut("verification/{token}")]
    public async ValueTask<IActionResult> VerificateAsync([FromRoute] string token)
    {
        var result = await _accountService.VerificeteAsync(token);
        
        return Ok(result);
    }
}
