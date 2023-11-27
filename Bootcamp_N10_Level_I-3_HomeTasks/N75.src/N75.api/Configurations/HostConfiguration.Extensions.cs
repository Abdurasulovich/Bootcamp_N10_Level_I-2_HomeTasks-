using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using N75.api.DataContexts;
using N75.api.Models.Settings;
using N75.api.Services;

namespace N75.api.Configurations;

public static partial class HostConfiguration
{
    private static WebApplicationBuilder AddNotifcationsInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<SmtpEmailSenderSetting>(
            builder.Configuration.GetSection(nameof(SmtpEmailSenderSetting)));

        builder.Services.Configure<NotificationSenderSetting>(
            builder.Configuration.GetSection(nameof(NotificationSenderSetting)));

        builder.Services.AddScoped<EmailSenderService>();

        //registering hosted service
        builder.Services.AddHostedService<EmailSenderBackgroundService>(provider =>
        {
            var scopedService = provider.CreateScope();

            return new EmailSenderBackgroundService(
                scopedService.ServiceProvider.GetRequiredService<IOptions<NotificationSenderSetting>>(),
                scopedService.ServiceProvider.GetRequiredService<EmailSenderService>());
        });

        return builder;
    }

    private static WebApplicationBuilder AddIdentityInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<IdentityDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("IdentityConnection")));

        builder.Services.AddScoped<UserService>().AddScoped<AccountAggregatorService>();

        return builder;
    }

    private static WebApplicationBuilder AddExposers(this WebApplicationBuilder builder)
    {
        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        builder.Services.AddControllers();

        return builder;
    }

    private static WebApplicationBuilder AddDevTools(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder;
    }

    private static WebApplication UseExposers(this WebApplication app)
    {
        app.MapControllers();

        return app;
    }

    private static WebApplication UseDevTools(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }
}
