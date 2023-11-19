using Microsoft.EntityFrameworkCore;
using N71.Blog.Api.Configurations;
using N71.Blog.Application.Identity.Services.Interfaces;
using N71.Blog.Persistance.DataContext;

var builder = WebApplication.CreateBuilder(args);
await builder.ConfigureAsync();


var app = builder.Build();
await app.ConfigureAsync();

var scope = app.Services.CreateScope().ServiceProvider;
var dbContext = scope.GetRequiredService<BlogsDbContext>();
var tokenGeneratorService = scope.GetRequiredService < IAccessTokenGeneratorService>();
var passwordHasherService = scope.GetRequiredService<IPasswordHasherService>();

// var adminPassword = passwordHasherService.HashPassword("Admin_Javlonbek");
// var readerPassword = passwordHasherService.HashPassword("Reader_Djalekeev");

// var user = await dbContext.Users.Include(user => user.Role).OrderBy(user => user.Id).LastOrDefaultAsync();
// var token = tokenGeneratorService.GetToken(user);
await app.RunAsync();