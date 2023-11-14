using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using N70.Application.Common.Identity.Services;
using N70.Application.Common.Notifocations.Services;
using N70.Application.Common.Notifocations.Settings;
using N70.Domain.Entities;
using N70.Infrastructure.Common.Identity.Services;
using N70.Infrastructure.Common.Notifications.Services;
using N70.Persistance.DataContexts;
using N70.Persistance.Repositories;
using N70.Persistance.Repositories.Interfaces;

namespace N70.Api.Configurations;

public static partial class HostConfiguration
{
    public static WebApplicationBuilder AddHttpContextProvider(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();
        return builder;
    }
    private static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<IdentityDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


        return builder;
    }

    private static WebApplicationBuilder AddIdentityInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(nameof(JwtSettings)));
        builder.Services.Configure<VerificationTokenSettings>(
            builder.Configuration.GetSection(nameof(VerificationTokenSettings)));

        builder.Services.AddDataProtection();

        builder.Services.AddTransient<ITokenGeneratorService, TokenGeneratorService>()
            .AddTransient<IPasswordHasherService, PasswordHasherService>()
            .AddTransient<IVerificationTokenGeneratorService, VerificationTokenGeneratorService>();

        builder.Services
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IRoleRepository, RoleRepository>()
            .AddScoped<IAccessTokenRepository, AccessTokenRepository>();

        builder.Services
            .AddScoped<IAccountService, AccountService>()
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<IRoleService, RoleService>()
            .AddScoped<IAccessTokenService, AccessTokenService>();

        var jwtSettings = new JwtSettings();
        builder.Configuration.GetSection(nameof(JwtSettings)).Bind(jwtSettings);

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = jwtSettings.ValidateIssure,
                    ValidIssuer = jwtSettings.ValidIssure,
                    ValidAudience = jwtSettings.ValidAudence,
                    ValidateAudience = jwtSettings.ValidateAudence,
                    ValidateLifetime = jwtSettings.ValidateLifeTime,
                    ValidateIssuerSigningKey = jwtSettings.ValidateIssureSigningKey,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                };

            });
        return builder;
    }

    private static WebApplicationBuilder AddNotificationInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<EmailSenderSettings>(builder.Configuration.GetSection(nameof(EmailSenderSettings)));

        builder.Services.AddScoped<IEmailOrchestrationService, EmailOrchestrationService>();

        return builder;
    }

    private static WebApplicationBuilder AddDevTools(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen();
        builder.Services.AddEndpointsApiExplorer();

        return builder;
    }

    private static WebApplicationBuilder AddExposers(this WebApplicationBuilder builder)
    {
        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        builder.Services.AddControllers();

        return builder;
    }
    private static WebApplication UserIdentityInfrastructure(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }

    private static WebApplication UseDevTools(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }

    private static WebApplication UseExposers(this WebApplication app)
    {
        app.MapControllers();
        return app;
    }
    
    
}