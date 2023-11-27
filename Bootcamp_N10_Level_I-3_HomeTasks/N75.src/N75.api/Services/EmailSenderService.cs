using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using N75.api.DataContexts;
using N75.api.Models.Common;
using N75.api.Models.Entites;
using N75.api.Models.Settings;
using System.Net;
using System.Net.Mail;

namespace N75.api.Services;

public class EmailSenderService(IOptions<SmtpEmailSenderSetting> smtpEmailSenderSetting,
    IOptions<NotificationSenderSetting> notificaitonSenderSetting,
    IdentityDbContext identityDbContext
    )
{
    private readonly SmtpEmailSenderSetting _smtpEmailSenderSetting = smtpEmailSenderSetting.Value;
    private readonly NotificationSenderSetting _notificationSenderSetting = notificaitonSenderSetting.Value;

    public async ValueTask<bool> UpdateEventAsync(
        NotificationEvent notificationEvent,
        CancellationToken cancellationToken = default
        )
    {
        identityDbContext.Update(notificationEvent);
        return await identityDbContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public async ValueTask<List<NotificationEvent>> GetFailedEventsAsync(int batchSize)
    {
        var failedNotificationEvents = new List<NotificationEvent>();

        //query failed email events
        failedNotificationEvents.AddRange(await identityDbContext.EmailNotificationEvents
            .Where(emailNotificationEvent => !emailNotificationEvent.IsCancelled &&
                                            !emailNotificationEvent.IsSuccessful &&
                                            emailNotificationEvent.ResentAttemps <=
                                            _notificationSenderSetting.ResendAttempsThreshold)
            .OrderBy(emailNotificationEvent => emailNotificationEvent.CreatedAt)
            .ToListAsync());

        //query failed sms event

        return failedNotificationEvents.Take(batchSize).ToList();
    }

    public async ValueTask<bool> QueueAsync(
        Guid receiverUserId,
        string emailAddress,
        string subject,
        string body,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
        )
    {
        var emailNotificationEvent = new EmailNotificationEvent
        {
            ReceiverUserId = receiverUserId,
            ReceiverEmailAddress = emailAddress,
            Subject = subject,
            Content = body,
            CreatedAt = DateTime.UtcNow
        };

        identityDbContext.Add(emailNotificationEvent);

        return !saveChanges | await identityDbContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public ValueTask<bool> SendAsync(
        string emailAddress,
        string subject,
        string body,
        CancellationToken cancellationToken = default
        )
    {
        var mail = new MailMessage(_smtpEmailSenderSetting.CredentialAddress, emailAddress);
        mail.Subject = subject;
        mail.Body = body;
        mail.IsBodyHtml = true;

        var smtpClient = new SmtpClient(_smtpEmailSenderSetting.Host, _smtpEmailSenderSetting.Port);
        smtpClient.Credentials =
            new NetworkCredential(_smtpEmailSenderSetting.CredentialAddress, _smtpEmailSenderSetting.Password);
        smtpClient.EnableSsl = true;
        smtpClient.Send(mail);

        return new ValueTask<bool>(true);
    }
}
