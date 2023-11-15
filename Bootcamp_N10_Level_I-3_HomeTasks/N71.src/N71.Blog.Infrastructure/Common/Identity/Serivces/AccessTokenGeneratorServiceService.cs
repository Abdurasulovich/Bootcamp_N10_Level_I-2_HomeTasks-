using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using N71.Blog.Application.Identity.Services.Interfaces;
using N71.Blog.Domain.Entity;

namespace N71.Blog.Infrastructure.Common.Identity.Serivces;

public class AccessTokenGeneratorService : IAccessTokenGenerator
{
    public string GetToken(User user)
    {
        throw new NotImplementedException();
    }

    public JwtSecurityToken GetJwtToken(User user)
    {
        throw new NotImplementedException();
    }

    public List<Claim> GetClaims(User user)
    {
        throw new NotImplementedException();
    }
}