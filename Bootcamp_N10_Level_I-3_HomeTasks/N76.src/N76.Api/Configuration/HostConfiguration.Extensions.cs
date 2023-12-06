using Microsoft.EntityFrameworkCore;
using N76.Api.DbContexts;
using N76.Api.Interceptors;
using N76.Api.Repositories;
using N76.Api.Repositories.Interfaces;
using N76.Api.Services;
using N76.Api.Services.Interfaces;

namespace N76.Api.Configuration;

public static partial class HostConfiguration
{
    private static WebApplicationBuilder AddDevTools(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen()
            .AddEndpointsApiExplorer();

        return builder;
    }

    private static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<UpdatePrimaryKeyInterceptor>()
            .AddScoped<UpdateAuditableInterceptor>()
            .AddScoped<UpdateSoftDeletedInterceptor>();

        return builder;
    }

    private static WebApplicationBuilder AddIdentityInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<IdentityDbContext>((provider, options) =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));

            options.AddInterceptors(
                provider.GetRequiredService<UpdatePrimaryKeyInterceptor>(),
                provider.GetRequiredService<UpdateAuditableInterceptor>(),
                provider.GetRequiredService<UpdateSoftDeletedInterceptor>());
        });

        builder.Services.AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IUserService, UserService>();

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

    private static WebApplication UseDevTools(this WebApplication app)
    {
        app.UseSwagger()
            .UseSwaggerUI();

        return app;
    }
}
