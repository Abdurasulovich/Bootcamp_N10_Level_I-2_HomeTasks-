namespace N71.Blog.Application.Identity.Services.Interfaces;

public interface IPasswordHasherService
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string hashPassword);
}