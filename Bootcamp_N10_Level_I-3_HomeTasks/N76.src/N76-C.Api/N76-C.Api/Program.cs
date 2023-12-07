using N76_C.Api.Configuration;
var builder = WebApplication.CreateBuilder(args);
await builder.ConfigureAsync();
var app = builder.Build();
await app.ConfigureAsync();
await app.RunAsync(); ;