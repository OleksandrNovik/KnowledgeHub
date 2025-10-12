using KnowledgeHub.Domain.Entities.Abstract;
using KnowledgeHub.Domain.Entities.User;

namespace KnowledgeHub.Domain.Entities.Knowledge;

public class KnowledgeEntity : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid UserId { get; set; }
    public UserEntity User { get; set; }
    public IList<KnowledgeSourceEntity> KnowledgeSources { get; set; }
    public IList<KnowledgeCategoryEntity> KnowledgeCategories { get; set; }
}