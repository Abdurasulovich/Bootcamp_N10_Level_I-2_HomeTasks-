using Caching.SimpleInfra.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);
await builder.ConfigureAsync();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await app.ConfigureAsync();
await app.RunAsync();
