namespace KnowledgeHub.Domain.Dtos.User;

/// <summary>
///     Properties that needed to generate Jwt token for the user
/// </summary>
public record UserIdentity(string Id, string Email, UserRole Role);