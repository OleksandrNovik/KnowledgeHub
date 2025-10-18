namespace KnowledgeHub.Domain.Dtos.User.Authorization;

public record UserInfo(string Username, string Email, UserRole Role);

public record UserAuthorizationResult(UserInfo UserInfo, JwtToken TokenInfo);