namespace KnowledgeHub.Domain.Dtos.User.Authorization;

/// <summary>
///     Properties that needed to generate Jwt token for the user
/// </summary>
public record UserIdentity(Guid Id, string Email, UserRole Role);