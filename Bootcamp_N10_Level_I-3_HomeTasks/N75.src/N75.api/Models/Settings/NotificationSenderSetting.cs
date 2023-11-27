namespace N75.api.Models.Settings;

public class NotificationSenderSetting
{
    public int ResendAttempsThreshold { get; set; }

    public int ResendIntervalInSeconds { get; set; }

    public int BatchSize { get; set; }

    public int BatchResendIntervalInSeconds { get; set; }
}
