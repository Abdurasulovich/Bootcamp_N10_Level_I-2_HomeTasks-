using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Text.RegularExpressions;
using Sms.Infrastructure.Application.Common.Enums;
using Sms.Infrastructure.Application.Notifications.Services;
using Sms.Infrastructure.Domain.Common.Exceptions;
using Sms.Infrastructure.Domain.Extensions;
using Twilio.Rest.Chat.V1.Service.Channel;

namespace Sms.Infrastructure.Infrastructure.Common.Notifications.Services;

public class SmsOrchestrationService : ISmsOrchestrationService
{

    private readonly ISmsSenderService _smsSenderService;

    public SmsOrchestrationService(ISmsSenderService smsSenderService)
    {
        _smsSenderService = smsSenderService;
    }
    public async ValueTask<FuncResult<bool>> SendAsync(
        string senderPhoneNumber, 
        string receiverPhoneNumber, 
        NotificationTemplateType templateType,
        Dictionary<string, string> variables, 
        CancellationToken cancellationToken)
    {
        var test = async () =>
        {
            var template = GetTemplate(templateType);
            var message = GetMessage(template, variables);

            await _smsSenderService.SendAsync(senderPhoneNumber, receiverPhoneNumber, message, cancellationToken);

            return true;


        };
        return await test.GetValueAsync();
    }

    public string GetTemplate(NotificationTemplateType templateType)
    {
        var template = templateType switch
        {
            NotificationTemplateType.SystemWelcomeNotification => "Welcome to the system, {{UserName}}",
            NotificationTemplateType.EmailVerificationNotification =>
                "Verify your email by clicking the link, {{VerificationLink}}",

            _ => throw new ArgumentOutOfRangeException(nameof(templateType), "")
        };

        return template;
    }

    public string GetMessage(string template, Dictionary<string, string> variables)
    {

        var messageBuilder = new StringBuilder(template);

        var pattern = @"\{\{([^\{\}]+)\}\}";
        var matchValuePattern = "{{(.*?)}}";

        var matches = Regex.Matches(template, pattern)
            .Select(match =>
            {
                var placeholder = match.Value;
                var placeholderValue = Regex.Match(placeholder, matchValuePattern).Groups[1].Value;

                var valid = variables.TryGetValue(placeholderValue, out var value);

                return new
                {
                    Placeholder = placeholder,
                    Vaule = value,
                    IsValid = valid
                };
            });
        foreach (var match in matches)
            messageBuilder.Replace(match.Placeholder, match.Vaule);

        var message = messageBuilder.ToString();
        return message;
    }
}