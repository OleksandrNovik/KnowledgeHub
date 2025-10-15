using KnowledgeHub.Domain.Entities.Abstract;

namespace KnowledgeHub.Application.Repositories.Generic;

/// <summary>
///     Methods for repository for create, update and delete operations
/// </summary>
/// <typeparam name="TEntity"> Type of entity that repository works with </typeparam>
public interface IUpdatableRepository<in TEntity> where TEntity : BaseEntity
{
    /// <summary>
    ///     Adds new entity to the table
    /// </summary>
    /// <param name="entity"> Entity that is added to the table </param>
    /// <param name="ct"> Cancellation token </param>
    public Task AddAsync(TEntity entity, CancellationToken ct);

    /// <summary>
    ///     Updates provided entity in the table
    /// </summary>
    /// <param name="entity"> Data that is updated </param>
    /// <param name="ct"> Cancellation token </param>
    public Task UpdateAsync(TEntity entity, CancellationToken ct);

    /// <summary>
    ///     Deletes record by id
    /// </summary>
    /// <param name="id"> Id of entity that is deleted </param>
    /// <param name="ct"> Cancellation token </param>
    public Task DeleteByIdAsync(Guid id, CancellationToken ct);

    /// <summary>
    ///     Deletes entity from database
    /// </summary>
    /// <param name="entity"> Entity that is deleted  </param>
    /// <param name="ct"> Cancellation token </param>
    public Task DeleteAsync(TEntity entity, CancellationToken ct);
}