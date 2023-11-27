using Microsoft.Extensions.Options;
using N75.api.Extensions;
using N75.api.Models.Common;
using N75.api.Models.Entites;
using N75.api.Models.Settings;
using Polly;

namespace N75.api.Services;

public class EmailSenderBackgroundService(
    IOptions<NotificationSenderSetting> notificationSenderSetting,
    EmailSenderService emailSenderService
    ) : BackgroundService
{
    private readonly NotificationSenderSetting _notificationSenderSetting = notificationSenderSetting.Value;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("Started background service");

        while (!stoppingToken.IsCancellationRequested)
        {
            var retryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(_notificationSenderSetting.ResendAttempsThreshold,
                _ => _notificationSenderSetting.ResendIntervalInSeconds > 0
                ? TimeSpan.FromSeconds(_notificationSenderSetting.ResendAttempsThreshold)
                : TimeSpan.Zero);

            var failedNotificationEvents =
                await emailSenderService.GetFailedEventsAsync(_notificationSenderSetting.BatchSize);
            await retryPolicy.ExecuteAsync(async () =>
                await ProcessFailedEventsAsync(failedNotificationEvents, stoppingToken));

            await Task.Delay(_notificationSenderSetting.BatchResendIntervalInSeconds, stoppingToken);
        }

        Console.WriteLine("Stopped background service");
    }

    private async ValueTask ProcessFailedEventsAsync(
        List<NotificationEvent> notificationEvent,
        CancellationToken cancellationToken = default
        )
    {
        var exception = default(Exception?);

        var notificationResults = notificationEvent.Select(async(notificationEvent)=>
        {
            if(notificationEvent is EmailNotificationEvent emailNotificationEvent)
            {
                var sendNotificationTask = async () =>
                    await emailSenderService.SendAsync(emailNotificationEvent.ReceiverEmailAddress,
                    emailNotificationEvent.Subject,
                    emailNotificationEvent.Content,
                    cancellationToken: cancellationToken);

                var sendNotificationResult = await sendNotificationTask.GetValueAsync();

                emailNotificationEvent.IsSuccessful = sendNotificationResult.IsSuccess;
                emailNotificationEvent.ResentAttemps++;

                var updateNotificationTask = async () =>
                await emailSenderService.UpdateEventAsync(emailNotificationEvent, cancellationToken);

                await updateNotificationTask.GetValueAsync();

                if (sendNotificationResult is { IsSuccess: false, Exception: not null })
                    exception = sendNotificationResult.Exception;
            }
        });

        await Task.WhenAll(notificationResults);
        notificationEvent.RemoveAll(result => result.IsSuccessful);

        if (exception is not null)
            throw exception;
    }

}
