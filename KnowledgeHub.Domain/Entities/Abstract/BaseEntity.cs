using System.ComponentModel.DataAnnotations;

namespace KnowledgeHub.Domain.Entities.Abstract;

public abstract class BaseEntity
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreationTime { get; set; } = DateTime.UtcNow;
    public DateTime? LastModificationTime { get; set; }
}