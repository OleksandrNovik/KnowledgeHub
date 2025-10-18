namespace KnowledgeHub.Domain.Dtos.User.Authorization;

/// <summary>
///     Model that contains jwt token information
/// </summary>
/// <param name="Token"> Token generated from <see cref="UserIdentity" /> information </param>
/// <param name="RecievedTime"> Creation time of the token </param>
/// <param name="Expires"> Expiration time of the token in seconds </param>
public record JwtToken(string Token, DateTime RecievedTime, long Expires);