using KnowledgeHub.Domain.Dtos.User;

namespace KnowledgeHub.Domain.Dtos;

/// <summary>
///     Properties that needed to generate Jwt token for the user
/// </summary>
public record UserIdentity
{
    /// <summary>
    ///     Username is used as subject
    /// </summary>
    public string Useraname { get; }

    /// <summary>
    ///     Role of the user is provided as claim
    /// </summary>
    public UserRole Role { get; }
}