using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using N71.Blog.Application.Identity.Constants;
using N71.Blog.Application.Identity.Services.Interfaces;
using N71.Blog.Application.Settings;
using N71.Blog.Domain.Entity;

namespace N71.Blog.Infrastructure.Common.Identity.Serivces;

public class AccessTokenGeneratorServiceService : IAccessTokenGeneratorService
{
    private readonly JwtSettings _jwtSettings;

    public AccessTokenGeneratorServiceService(IOptions<JwtSettings> jwtSettings) => _jwtSettings = jwtSettings.Value;
    public string GetToken(User user)
    {
        var jwtToken = GetJwtToken(user);
        var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

        return token;
    }

    public JwtSecurityToken GetJwtToken(User user)
    {
        var claims = GetClaims(user);

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.ValidIssuer,
            audience: _jwtSettings.ValidAudience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationTimeInMinutes),
            signingCredentials: credential);

        return token;
    }

    public List<Claim> GetClaims(User user)
        => new List<Claim>
        {
            new(ClaimTypes.Email, user.EmailAddress),
            new(ClaimConstants.UserId, user.Id.ToString()),
            new(ClaimTypes.Role, user.Role.Type.ToString())
        };
}