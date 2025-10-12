using KnowledgeHub.Domain.Entities.Abstract;

namespace KnowledgeHub.Domain.Entities.Knowledge;

public class KnowledgeCategoryEntity : BaseEntity
{
    public Guid CategoryId { get; set; }
    public CategoryEntity Category { get; set; }
    public Guid KnowledgeId { get; set; }
    public KnowledgeEntity Knowledge { get; set; }
}