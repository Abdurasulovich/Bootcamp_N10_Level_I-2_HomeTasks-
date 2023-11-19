using FluentValidation;
using Notification.Infrastructure.Application.Common.Notifications.Brokers;
using Notification.Infrastructure.Application.Common.Notifications.Models;
using Notification.Infrastructure.Application.Common.Notifications.Services;
using Notification.Infrastructure.Domain.Enums;
using Notification.Infrastructure.Domain.Extensions;
using Twilio.Rest.Serverless.V1.Service.Asset;

namespace Notification.Infrastructure.Infrastructure.Common.Notificaiton.Services;

public class EmailSenderService : IEmailSenderService
{
    private readonly IEnumerable<IEmailSenderBroker> _emailSenderBrokers;
    private readonly IValidator<EmailMessage> _emailMessageValidator;

    public EmailSenderService(IEnumerable<IEmailSenderBroker> emailSenderBrokers,
        IValidator<EmailMessage> emailMessageValidator
        )
    {
        _emailSenderBrokers = emailSenderBrokers;
        _emailMessageValidator = emailMessageValidator;
    }
    public async ValueTask<bool> SendAsync(EmailMessage emailMessage, CancellationToken cancellationToken = default)
    {
        var validationResult = _emailMessageValidator.Validate(emailMessage,
            options => options.IncludeRuleSets(NotificationEvent.OnSending.ToString()));
        if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

        foreach (var emailSenderBroker in _emailSenderBrokers)
        {
            var sendNotificationTask = () => emailSenderBroker.SendAsync(emailMessage, cancellationToken);
            var result = await sendNotificationTask.GetValueAsync();

            emailMessage.IsSuccessful = result.IsSuccess;
            emailMessage.ErrorMessage = result.Exception?.Message;
            return result.IsSuccess;
        }
        return false;
    }
}
