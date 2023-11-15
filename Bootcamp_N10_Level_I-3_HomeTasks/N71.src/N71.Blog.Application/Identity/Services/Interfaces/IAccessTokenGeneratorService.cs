using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using N71.Blog.Domain.Entity;

namespace N71.Blog.Application.Identity.Services.Interfaces;

public interface IAccessTokenGenerator
{
    string GetToken(User user);

    JwtSecurityToken GetJwtToken(User user);
    List<Claim> GetClaims(User user);
}