using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KnowledgeHub.Application.Services.User;
using KnowledgeHub.Domain.Dtos.User.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace KnowledgeHub.Infrastructure.Services.User;

/// <summary>
///     Implementation of services that does Jwt token generation
/// </summary>
/// <param name="configuration"> App configuration to get secret, audience and issuer </param>
public class JwtService(IConfiguration configuration) : IJwtService
{
    /// <inheritdoc />
    public JwtToken? GenerateJwtToken(UserIdentity userIdentity)
    {
        JwtToken? jwt = null;

        try
        {
            var creationTime = DateTime.UtcNow;
            var expirationOffset = configuration.GetValue<long>("Jwt:ExpiresInSeconds", 1800);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userIdentity.Id.ToString()),
                new Claim(ClaimTypes.Email, userIdentity.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, userIdentity.Role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(configuration["Jwt:Issuer"], configuration["Jwt:Audience"], claims,
                expires: creationTime.AddSeconds(expirationOffset),
                signingCredentials: credentials);

            var stringToken = new JwtSecurityTokenHandler().WriteToken(token);
            jwt = new JwtToken(stringToken, creationTime, expirationOffset);
        }
        catch (Exception ex)
        {
            //TODO: add logging 
        }

        return jwt;
    }
}