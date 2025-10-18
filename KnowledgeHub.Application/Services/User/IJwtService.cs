using KnowledgeHub.Domain.Dtos.User;

namespace KnowledgeHub.Application.Services.User;

public interface IJwtService
{
    /// <summary>
    ///     Generates JWT token for provided user data
    /// </summary>
    /// <param name="userIdentity"> User identity data needed to generate token </param>
    /// <returns> Generated JWT token, will return null if something went wrong </returns>
    public JwtToken? GenerateJwtToken(UserIdentity userIdentity);
}