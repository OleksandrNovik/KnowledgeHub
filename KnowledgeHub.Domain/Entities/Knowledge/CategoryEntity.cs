using KnowledgeHub.Domain.Entities.Abstract;

namespace KnowledgeHub.Domain.Entities.Knowledge;

public class CategoryEntity : BaseNamedEntity
{ 
    public IList<KnowledgeCategoryEntity> KnowledgeCategories { get; set; }
}