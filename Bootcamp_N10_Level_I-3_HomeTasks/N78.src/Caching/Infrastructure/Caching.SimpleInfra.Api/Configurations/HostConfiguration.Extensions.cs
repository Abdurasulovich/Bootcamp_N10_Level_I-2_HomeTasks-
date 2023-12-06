using Caching.SimpleInfra.Api.Data;
using Caching.SimpleInfra.Application.Common.Identity.Services;
using Caching.SimpleInfra.Infrastructure.Common.Caching;
using Caching.SimpleInfra.Infrastructure.Common.Identity.Services;
using Caching.SimpleInfra.Infrastructure.Common.Settings;
using Caching.SimpleInfra.Persistence.Caching;
using Caching.SimpleInfra.Persistence.DataContexts;
using Caching.SimpleInfra.Persistence.Repostiories;
using Caching.SimpleInfra.Persistence.Repostiories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Caching.SimpleInfra.Api.Configurations;

public static partial class HostConfiguration
{
    private static WebApplicationBuilder AddCaching(this WebApplicationBuilder builder)
    {
        //register cache settings
        builder.Services.Configure<CacheSettings>(builder.Configuration.GetSection(nameof(CacheSettings)));

        //register lazy memory cache
        //builder.Services.AddLazyCache();
        builder.Services.AddStackExchangeRedisCache(
            options =>
            {
                options.Configuration = builder.Configuration.GetConnectionString("RedisConnectionString");
                options.InstanceName = "Caching.SimpleInfra";
            }
            );
        //builder.Services.AddSingleton<ICacheBroker, LazyMemoryCacheBroker>();
        builder.Services.AddSingleton<ICacheBroker, RedisDistributedCacheBroker>();

        return builder;
    }

    private static WebApplicationBuilder AddIdentityInfrastructure(this WebApplicationBuilder builder)
    {
        //register dbContexts
        builder.Services.AddDbContext<IdentityDbContext>(
            options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

        //register repositories
        builder.Services.AddScoped<IUserRepository, UserRepository>();

        //register foundation data access service
        builder.Services.AddScoped<IUserService, UserService>();

        return builder;
    }

    private static WebApplicationBuilder AddExposers(this WebApplicationBuilder builder)
    {
        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        builder.Services.AddControllers();

        return builder;
    }

    private static async ValueTask<WebApplication> SeedDataAsync(this WebApplication app)
    {
        var serviceScope = app.Services.CreateScope();
        await serviceScope.ServiceProvider.InitializeSeedAsync();

        return app;
    }

    private static WebApplication UseExposers(this WebApplication app)
    {
        app.MapControllers();

        return app;
    }
}
