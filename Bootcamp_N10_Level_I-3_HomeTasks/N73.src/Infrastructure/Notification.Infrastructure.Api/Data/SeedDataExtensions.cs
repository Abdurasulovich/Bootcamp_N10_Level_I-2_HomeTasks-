using Microsoft.EntityFrameworkCore;
using Notification.Infrastructure.Domain.Entities;
using Notification.Infrastructure.Domain.Enums;
using Notification.Infrastructure.Persistance.DataContexts;
using NUnit.Framework.Internal.Execution;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Twilio.Rest.Preview.DeployedDevices;

namespace Notification.Infrastructure.Api.Data;

public static class SeedDataExtensions
{
    public static async ValueTask InitializeSeedAsync(
        this IServiceProvider serviceProvider,
        IWebHostEnvironment webHostEnvironment
        )
    {
        var notificationDbContext = serviceProvider.GetRequiredService<NotificationDbContext>();

        if(!await notificationDbContext.EmailTemplates.AnyAsync())
            await notificationDbContext.SeedEmailTemplates(webHostEnvironment);

        if (!await notificationDbContext.SmsTemplates.AnyAsync())
            await notificationDbContext.SeedSmsTemplates();

        if (!await notificationDbContext.Users.AnyAsync())
            await notificationDbContext.SeedUserAsync();

        if (!await notificationDbContext.UserSettings.AnyAsync())
            await notificationDbContext.SeedUserSettingsAsync();

        if (notificationDbContext.ChangeTracker.HasChanges())
            await notificationDbContext.SaveChangesAsync();
    }

    private static async ValueTask SeedEmailTemplates(
        this NotificationDbContext notificationDbContext,
        IWebHostEnvironment webHostEnvironment
        )
    {
        var emailTemplateTypes = new List<NotificationTemplateType>
        {
            NotificationTemplateType.WelcomeNotification,
            NotificationTemplateType.EmailAddressVerificationNotification,
            NotificationTemplateType.ReferralNotification
        };

        var emailTemplateContents = await Task.WhenAll(emailTemplateTypes.Select(async templateType =>
        {
            var filePath = Path.Combine(webHostEnvironment.ContentRootPath,
                "Data",
                "EmailTemplates",
                Path.ChangeExtension(templateType.ToString(), "html"));
            return (TemplateType: templateType, TemplateContent: await File.ReadAllTextAsync(filePath));
        }));

        var emailTemplates = emailTemplateContents.Select(templateContent => templateContent.TemplateType switch
        {
            NotificationTemplateType.WelcomeNotification => new EmailTemplate
            {
                TemplateType = templateContent.TemplateType,
                Subject = "Welcome to our service!",
                Content = templateContent.TemplateContent,
                
            },
            NotificationTemplateType.EmailAddressVerificationNotification => new EmailTemplate
            {
                TemplateType = templateContent.TemplateType,
                Subject = "Confirm your email address",
                Content = templateContent.TemplateContent
            },
            NotificationTemplateType.ReferralNotification => new EmailTemplate
            {
                TemplateType = templateContent.TemplateType,
                Subject = "You have been referred!",
                Content = templateContent.TemplateContent
            },
            _ => throw new NotSupportedException("Template type not supported.")
        });

        await notificationDbContext.EmailTemplates.AddRangeAsync(emailTemplates);
    }

    private static async ValueTask SeedSmsTemplates(this NotificationDbContext notificationDbContext)
    {
        await notificationDbContext.SmsTemplates.AddRangeAsync(new SmsTemplate
        {
            TemplateType = NotificationTemplateType.WelcomeNotification,
            Content =
            "Welcome {{UserName}}! We're thrilled to have you on board. Get ready to explore and enjoy our services."
        },
        new SmsTemplate
        {
            TemplateType = NotificationTemplateType.PhoneNumberVerificationNotification,
            Content =
            "Hey {{UserName}}. To secure your account, please verify your phone number using this link: {{PhoneNumberVerificationLink}}"
        },
        new SmsTemplate
        {
            TemplateType = NotificationTemplateType.ReferralNotification,
            Content =
            "You've been invited to join by  a friend {{SenderName}}! Sign up today and enjoy exclusive benefits. Use referral code."
        });
    }

    private static async ValueTask SeedUserAsync(this NotificationDbContext notificationDbContext)
    {
        await notificationDbContext.Users.AddRangeAsync(new User
        {
            UserName = "System",
            PhoneNumber = "+12132931337",
            EmailAddress = "javaengeineer@gmail.com",
            Role = RoleType.System
        },
        new User
        {
            Id = Guid.Parse("9D4B3553-9AED-44BA-AF0F-5B0C7771A9BF"),
            UserName = "John",
            PhoneNumber = "+998913774506",
            EmailAddress = "javaengeineer@gmail.com",
        },
        new User
        {
            Id = Guid.Parse("1926B0C4-D77F-4644-99A0-EEF1ADF8E055"),
            UserName = "Jeck",
            PhoneNumber = "+12132921338",
            EmailAddress = "djalekeev@gmail.com"
        });
    }

    private static async ValueTask SeedUserSettingsAsync(this NotificationDbContext notificationDbContext)
    {
        await notificationDbContext.UserSettings.AddRangeAsync(new UserSettings
        {
            Id = Guid.Parse("7F7631AF-798D-49B2-B4CD-DE8B2FBF0962"),
            PreferredNotificationType = NotificationType.Sms
        },
        new UserSettings
        {
            Id = Guid.Parse("94379744-55B1-47CE-A646-C9A9850C67EB"),
            PreferredNotificationType = NotificationType.Sms
        });
    }
}
