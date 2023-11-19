using BC = BCrypt.Net.BCrypt;
using N71.Blog.Application.Identity.Services.Interfaces;
using N71.Blog.Domain.Entity;


namespace N71.Blog.Infrastructure.Common.Identity.Serivces;

public class PasswordHasherService : IPasswordHasherService
{
    public string HashPassword(string password)
        => BC.HashPassword(password);


    public bool VerifyPassword(string password, string hashPassword)
        => BC.Verify(password, hashPassword);
}