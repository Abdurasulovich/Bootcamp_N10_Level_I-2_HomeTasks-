namespace N75.api.Configurations;

public static partial class HostConfiguration
{
    public static ValueTask<WebApplicationBuilder> ConfigureAsync(this WebApplicationBuilder builder)
    {
        builder.AddNotifcationsInfrastructure()
                .AddIdentityInfrastructure()
                .AddExposers()
                .AddDevTools();

        return new(builder);
    }

    public static ValueTask<WebApplication> ConfigureAsync(this WebApplication app)
    {
        app.UseExposers()
            .UseDevTools();

        return new(app);
    }
}
