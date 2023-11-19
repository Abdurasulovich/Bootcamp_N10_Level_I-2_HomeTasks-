using Sms.Infrastructure.Application.Notifications.Brokers;
using Sms.Infrastructure.Application.Notifications.Services;
using Sms.Infrastructure.Infrastructure.Common.Notifications.Brokers;
using Sms.Infrastructure.Infrastructure.Common.Notifications.Services;

namespace Sms.Infrastructure.Api.Configurations;

public static partial class HostConfiguration
{
    private static WebApplicationBuilder AddNotificationInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ISmsSenderBroker, TwilioSmsSenderBroker>();


        builder.Services.AddScoped<ISmsSenderService, SmsSenderService>();

        builder.Services
            .AddScoped<ISmsOrchestrationService, SmsOrchestrationService>()
            .AddScoped<INotificationAggregatorService, NotificationAggregatorService>();

        return builder;
    }

    private static WebApplicationBuilder AddExposers(this WebApplicationBuilder builder)
    {
        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        builder.Services.AddControllers();
        return builder;
        
    }

    private static WebApplication UseExposers(this WebApplication app)
    {
        app.MapControllers();
        return app;
    }
    
}