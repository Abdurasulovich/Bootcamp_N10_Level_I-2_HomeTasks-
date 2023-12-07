namespace N76_C.Api.Configuration;

public static partial class HostConfiguration
{
    public static ValueTask<WebApplicationBuilder> ConfigureAsync(this WebApplicationBuilder builder)
    {
        builder.AddIdentityInfrastructure()
            .AddDevTools()
            .AddPersistence()
            .AddExposers();

        return new(builder);
    }

    public static async ValueTask<WebApplication> ConfigureAsync(this WebApplication app)
    {
        await app.SeedDataAsync();
        app.UseExposers()
            .UseDevTools();

        return app;
    }
}
