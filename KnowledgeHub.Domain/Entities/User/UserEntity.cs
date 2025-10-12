using KnowledgeHub.Domain.Entities.Abstract;
using KnowledgeHub.Domain.Entities.Knowledge;

namespace KnowledgeHub.Domain.Entities.User;

public enum UserRole
{
    Default,
    Admin
}

public class UserEntity : BaseEntity
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public UserRole Role { get; set; }
    public IList<KnowledgeEntity> KnowledgeItems { get; set; } = [];
}