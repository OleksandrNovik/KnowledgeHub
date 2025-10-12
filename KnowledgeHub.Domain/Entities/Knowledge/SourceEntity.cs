using KnowledgeHub.Domain.Entities.Abstract;

namespace KnowledgeHub.Domain.Entities.Knowledge;

public class SourceEntity : BaseNamedEntity
{
    public string Url { get; set; }
    public IList<KnowledgeSourceEntity> KnowledgeSources { get; set; }
}