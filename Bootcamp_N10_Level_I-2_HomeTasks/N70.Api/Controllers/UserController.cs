using Microsoft.AspNetCore.Mvc;
using N70.Application.Common.Identity.Services;

namespace N70.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
}