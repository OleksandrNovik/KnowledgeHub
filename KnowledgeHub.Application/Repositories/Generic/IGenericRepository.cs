using KnowledgeHub.Domain.Entities.Abstract;

namespace KnowledgeHub.Application.Repositories.Generic;

/// <summary>
///     Generic implementation of repository that can read and update entities
/// </summary>
/// <typeparam name="TEntity"> Type of entity that repository works with </typeparam>
public interface IGenericRepository<TEntity> : IReadableRepository<TEntity>, IUpdatableRepository<TEntity>
    where TEntity : BaseEntity;