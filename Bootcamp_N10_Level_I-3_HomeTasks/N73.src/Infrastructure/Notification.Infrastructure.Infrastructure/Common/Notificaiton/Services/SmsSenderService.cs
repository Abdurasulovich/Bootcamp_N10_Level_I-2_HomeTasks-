using FluentValidation;
using Notification.Infrastructure.Application.Common.Notifications.Brokers;
using Notification.Infrastructure.Application.Common.Notifications.Models;
using Notification.Infrastructure.Application.Common.Notifications.Services;
using Notification.Infrastructure.Domain.Enums;
using Notification.Infrastructure.Domain.Extensions;

namespace Notification.Infrastructure.Infrastructure.Common.Notificaiton.Services;

public class SmsSenderService : ISmsSenderService
{
    private IEnumerable<ISmsSenderBroker> _smsSenderBrokers;
    private IValidator<SmsMessage> _smsMessageValidator;

    public SmsSenderService(
        IEnumerable<ISmsSenderBroker> smsSenderBrokers,
        IValidator<SmsMessage> smsMessageValidator
        )
    {
        _smsSenderBrokers = smsSenderBrokers;
        _smsMessageValidator = smsMessageValidator;
    }
    public async ValueTask<bool> SendAsync(SmsMessage smsMessage, CancellationToken cancellationToken = default)
    {
        var validationResult = _smsMessageValidator.Validate(smsMessage,
            options => options.IncludeRuleSets(NotificationEvent.OnRendering.ToString()));

        if (!validationResult.IsValid) 
            throw new ValidationException(validationResult.Errors);

        foreach(var smsSenderBroker in _smsSenderBrokers)
        {
            var sendNotificationTask = () => smsSenderBroker.SendAsync(smsMessage, cancellationToken);

            var result = await sendNotificationTask.GetValueAsync();

            smsMessage.IsSuccessful = result.IsSuccess;
            smsMessage.ErrorMessage = result.Exception?.Message;
            return result.IsSuccess;
        }

        return false;
    }
}
