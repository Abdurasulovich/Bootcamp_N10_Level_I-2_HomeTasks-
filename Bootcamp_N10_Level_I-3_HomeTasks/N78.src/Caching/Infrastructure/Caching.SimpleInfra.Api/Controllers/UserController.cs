using Caching.SimpleInfra.Application.Common.Extensions;
using Caching.SimpleInfra.Application.Common.Identity.Services;
using Caching.SimpleInfra.Application.Common.Querying;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Caching.SimpleInfra.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpGet]
    public async ValueTask<IActionResult> GetById([FromQuery] FilterPagination paginationOptions)
    {
        var result = await userService.Get(asNoTracking: true).ApplyPagination(paginationOptions).ToListAsync();
        return result.Any() ? Ok(result) : NotFound();
    }
    [HttpGet("{userId:guid}")]
    public async ValueTask<IActionResult> GetById([FromRoute] Guid userId)
    {
        var result = await userService.GetByIdAsync(userId);
        return result is not null ? Ok(result) : NotFound();
    }
}
