using KnowledgeHub.Domain.Entities.Abstract;

namespace KnowledgeHub.Domain.Entities.Knowledge;

public class KnowledgeSourceEntity : BaseEntity
{
    public Guid KnowledgeSourceId { get; set; }
    public SourceEntity Source { get; set; }
    public Guid KnowledgeId { get; set; }
    public KnowledgeEntity Knowledge{ get; set; }
}