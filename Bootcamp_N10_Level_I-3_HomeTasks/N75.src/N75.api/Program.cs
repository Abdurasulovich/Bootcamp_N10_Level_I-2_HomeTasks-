using N75.api.Configurations;
using N75.api.Models.Entites;
using N75.api.Services;

var builder = WebApplication.CreateBuilder(args);
await builder.ConfigureAsync();

var app = builder.Build();

app.MapPost("api/users",
    async (User user, AccountAggregatorService accountService) => { await accountService.CreateAsync(user); });


await app.ConfigureAsync();
await app.RunAsync();