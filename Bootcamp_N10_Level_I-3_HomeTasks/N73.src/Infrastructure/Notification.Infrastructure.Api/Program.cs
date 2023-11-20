using Notification.Infrastructure.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
await builder.ConfigureAsync();

var app = builder.Build();

await app.ConfigureAsync();
await app.RunAsync();