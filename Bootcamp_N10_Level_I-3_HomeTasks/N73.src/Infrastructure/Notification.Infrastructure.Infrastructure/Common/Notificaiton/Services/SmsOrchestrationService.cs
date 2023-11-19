using AutoMapper;
using Notification.Infrastructure.Application.Common.Identity.Services;
using Notification.Infrastructure.Application.Common.Notifications.Models;
using Notification.Infrastructure.Application.Common.Notifications.Services;
using Notification.Infrastructure.Domain.Common.Exceptions;
using Notification.Infrastructure.Domain.Entities;
using Notification.Infrastructure.Domain.Enums;
using Notification.Infrastructure.Domain.Extensions;
using Notification.Infrastructure.Persistance.DataContexts;

namespace Notification.Infrastructure.Infrastructure.Common.Notificaiton.Services;

public class SmsOrchestrationService : ISmsOrchestrationService
{
    private readonly IMapper _mapper;
    private readonly ISmsRenderingService _smsRenderingService;
    private readonly ISmsTemplateService _smsTemplateService;
    private readonly ISmsSenderService _smsSenderService;
    private readonly ISmsHistoryService _smsHistoryService;
    private readonly IUserService _userService;
    private readonly NotificationDbContext _dbContext;

    public SmsOrchestrationService(
        IMapper mapper,
        ISmsTemplateService smsTemplateService,
        ISmsRenderingService smsRenderingService,
        ISmsSenderService smsSenderService,
        ISmsHistoryService smsHistoryService,
        NotificationDbContext dbContext,
        IUserService userService
        )
    {
        _mapper = mapper;
        _smsRenderingService = smsRenderingService;
        _smsTemplateService = smsTemplateService;
        _smsSenderService = smsSenderService;
        _smsHistoryService = smsHistoryService;
        _dbContext = dbContext;
        _userService = userService;
    }
    public async ValueTask<FuncResult<bool>> SendAsync(
        SmsNotificationRequest request, 
        CancellationToken cancellationToken = default)
    {
        var sendNotificationRequest = async () =>
        {
            var message = _mapper.Map<SmsMessage>(request);
            var senderUser =
            (await _userService.GetByIdAsync(request.SenderUserId!.Value, cancellationToken: cancellationToken))!;

            var receiverUser =

            (await _userService.GetByIdAsync(request.ReceiverUserId, cancellationToken: cancellationToken))!;

            message.SenderPhoneNumber = senderUser.PhoneNumber;
            message.ReceiverPhoneNumber = receiverUser.PhoneNumber;


            message.Template =
            await _smsTemplateService.GetByTypeAsync(request.TemplateType, true, cancellationToken) ??
            throw new InvalidOperationException(
                $"Invalid template for sending {NotificationType.Sms} notification");


            await _smsRenderingService.RenderAsync(message, cancellationToken);

            await _smsSenderService.SendAsync(message, cancellationToken);

            var history = _mapper.Map<SmsHistory>(message);
            var test = _dbContext.Entry(history.Template).State;

            await _smsHistoryService.CreateAsync(history, cancellationToken: cancellationToken);

            return history.IsSuccessful;
        };

        return await sendNotificationRequest.GetValueAsync();
    }
}
