using Microsoft.AspNetCore.Mvc;
using Sms.Infrastructure.Application.Notifications.Models;
using Sms.Infrastructure.Application.Notifications.Services;

namespace Sms.Infrastructure.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class NotificationController : ControllerBase
{
    private readonly INotificationAggregatorService _notificationAggregatorService;

    public NotificationController(INotificationAggregatorService notification)
    {
        _notificationAggregatorService = notification;
    }

    [HttpPost]
    public async ValueTask<IActionResult> Send([FromBody] NotificationRequest notificationRequest)
    {
        var result = await _notificationAggregatorService.SendAsync(notificationRequest);
        return result.IsSuccess ? Ok() : BadRequest();
    }
}