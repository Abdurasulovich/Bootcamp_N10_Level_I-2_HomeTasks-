using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using N70.Application.Common.Notifocations.Services;
using N70.Application.Common.Notifocations.Settings;

namespace N70.Infrastructure.Common.Notifications.Services;

public class EmailOrchestrationService : IEmailOrchestrationService
{
    private readonly EmailSenderSettings _emailSender;

    public EmailOrchestrationService(IOptions<EmailSenderSettings> emailSender)
    {
        _emailSender = emailSender.Value;
    }
    
    public ValueTask<bool> SendAsync(string emailAddress, string message)
    {
        var mail = new MailMessage(_emailSender.CredentialAddress, emailAddress);
        mail.Subject = "Siz registratsiyadan muvaffaqiyatli o'tdingiz!😁";
        mail.Body = message;

        var smtpClient = new SmtpClient(_emailSender.Host, _emailSender.Port);
        smtpClient.Credentials = new NetworkCredential(_emailSender.CredentialAddress, _emailSender.Password);
        smtpClient.EnableSsl = true;

        return new(true);

    }
}