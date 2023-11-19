namespace Notification.Infrastructure.Infrastructure.Common.Settings;

public class TwilioSmsSenderSettings
{
    public string AccountsId { get; set; } = default!;

    public string AuthToken { get; set; } = default!; 

    public string SenderPhoneNumber { get; set; } = default!;
}
