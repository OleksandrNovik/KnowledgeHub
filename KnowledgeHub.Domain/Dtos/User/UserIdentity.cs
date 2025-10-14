namespace KnowledgeHub.Domain.Dtos.User;

/// <summary>
///     Properties that needed to generate Jwt token for the user
/// </summary>
public record UserIdentity(string Username, UserRole Role)
{
    /// <summary>
    ///     Username is used as subject
    /// </summary>
    public string Username { get; } = Username;

    /// <summary>
    ///     Role of the user is provided as claim
    /// </summary>
    public UserRole Role { get; } = Role;
}