using AutoMapper;
using Notification.Infrastructure.Application.Common.Identity.Services;
using Notification.Infrastructure.Application.Common.Notifications.Models;
using Notification.Infrastructure.Application.Common.Notifications.Services;
using Notification.Infrastructure.Domain.Common.Exceptions;
using Notification.Infrastructure.Domain.Entities;
using Notification.Infrastructure.Domain.Enums;
using Notification.Infrastructure.Domain.Extensions;

namespace Notification.Infrastructure.Infrastructure.Common.Notificaiton.Services;

public class EmailOrchestrationService : IEmailOrchestrationService
{
    private readonly IMapper _mapper;
    private readonly IEmailTemplateService _emailTemplateService;
    private readonly IEmailRenderingService _emailRenderingService;
    private readonly IEmailSenderService _emailSenderService;
    private readonly IEmailHistoryService _emailHistoryService;
    private readonly IUserService _userService;

    public EmailOrchestrationService(
        IMapper mapper,
        IEmailTemplateService emailTemplateService,
        IEmailRenderingService emailRenderingService,
        IEmailSenderService emailSenderService,
        IEmailHistoryService emailHistoryService,
        IUserService userService)
    {
        _mapper = mapper;
        _emailTemplateService = emailTemplateService;
        _emailRenderingService = emailRenderingService;
        _emailSenderService = emailSenderService;
        _emailHistoryService = emailHistoryService;
        _userService = userService;
    }
    public async ValueTask<FuncResult<bool>> SendAsync(
        EmailNotificationRequest request, 
        CancellationToken cancellationToken = default)
    {
        var sendNotificationRequest = async () =>
        {
            var message = _mapper.Map<EmailMessage>(request);
            //get users
            //set receiver email address and sender email address

            var senderUser = (await _userService
            .GetByIdAsync(request.SenderUserId!.Value, cancellationToken: cancellationToken))!;

            var receiverUser = (await _userService
            .GetByIdAsync(request.ReceiverUserId, cancellationToken: cancellationToken))!;

            message.SendEmailAddress = senderUser.EmailAddress;
            message.ReceiverEmailAddress = receiverUser.EmailAddress;

            //get template

            message.Template =
            await _emailTemplateService.GetByTypeAsync(request.TemplateType, true, cancellationToken) ??
            throw new InvalidOperationException(
                $"Invalid template for sending {NotificationType.Email} notification.");

            //render template

            await _emailRenderingService.RenderAsync(message, cancellationToken);

            //send message
            await _emailSenderService.SendAsync(message, cancellationToken);

            //save history
            var history = _mapper.Map<EmailHistory>(message);

            await _emailHistoryService.CreateAsync(history, cancellationToken: cancellationToken);

            return history.IsSuccessful;
        };

        return await sendNotificationRequest.GetValueAsync();
    }
}
