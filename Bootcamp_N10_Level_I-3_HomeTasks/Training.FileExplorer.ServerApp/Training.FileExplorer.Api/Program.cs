using Training.FileExplorer.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

await builder.ConfigureAsync();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
await app.ConfigureAsync();
await app.RunAsync();
app.Run();
