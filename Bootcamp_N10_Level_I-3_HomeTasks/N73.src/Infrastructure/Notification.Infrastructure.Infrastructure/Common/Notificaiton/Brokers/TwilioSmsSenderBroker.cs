using Notification.Infrastructure.Application.Common.Notifications.Brokers;
using Notification.Infrastructure.Application.Common.Notifications.Models;
using Notification.Infrastructure.Infrastructure.Common.Settings;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Notification.Infrastructure.Infrastructure.Common.Notificaiton.Brokers;

public class TwilioSmsSenderBroker : ISmsSenderBroker
{
    private readonly TwilioSmsSenderSettings _twilioSmsSenderSettings;
    public TwilioSmsSenderBroker(TwilioSmsSenderSettings twilioSmsSenderSettings)
    { 
        _twilioSmsSenderSettings = twilioSmsSenderSettings; 
    }

    public ValueTask<bool> SendAsync(SmsMessage smsMessage, CancellationToken cancellationToken = default)
    {
        TwilioClient.Init(_twilioSmsSenderSettings.AccountsId, _twilioSmsSenderSettings.AuthToken);

        var messageContent = MessageResource.Create(
            body: smsMessage.Message,
            from: new Twilio.Types.PhoneNumber(_twilioSmsSenderSettings.SenderPhoneNumber),
            to: new Twilio.Types.PhoneNumber(smsMessage.ReceiverPhoneNumber)
            );

        return new ValueTask<bool>(true);
    }
}
