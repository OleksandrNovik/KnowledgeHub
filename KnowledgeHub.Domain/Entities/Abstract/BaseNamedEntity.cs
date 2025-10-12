namespace KnowledgeHub.Domain.Entities.Abstract;

/// <summary>
/// Entity with name property
/// </summary>
public class BaseNamedEntity : BaseEntity
{
    public string Name { get; set; }
}