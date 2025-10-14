using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KnowledgeHub.Application.Services.User;
using KnowledgeHub.Domain.Dtos.User;
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
    public string? GenerateJwtToken(UserIdentity userIdentity)
    {
        string? jwt = null;

        try
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userIdentity.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, userIdentity.Role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(configuration["Jwt:Issuer"], configuration["Jwt:Audience"], claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            jwt = new JwtSecurityTokenHandler().WriteToken(token);
        }
        catch (Exception ex)
        {
            //TODO: add logging 
        }

        return jwt;
    }
}