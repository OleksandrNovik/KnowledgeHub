using System.Linq.Expressions;
using KnowledgeHub.Domain.Entities.Abstract;

namespace KnowledgeHub.Application.Repositories.Generic;

/// <summary>
///     Declares repository with read only operations
/// </summary>
/// <typeparam name="TEntity"> Type of entity, used in the repository </typeparam>
public interface IReadableRepository<TEntity> where TEntity : BaseEntity
{
    /// <summary>
    ///     Gets general data about all entities from database. Does not include any additional joins
    /// </summary>
    /// <param name="ct"> Cancellation token </param>
    /// <returns> List of all entities from the table </returns>
    public Task<IList<TEntity>> GetAllAsync(CancellationToken ct = default);

    /// <summary>
    ///     Gets first item by provided predicate
    /// </summary>
    /// <param name="predicate"> Condition to find element </param>
    /// <param name="ct"> Cancellation token </param>
    /// <returns> Found entity by the condition, or null if there is no such item</returns>
    public Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken ct = default);

    /// <summary>
    ///     Gets first item with provided id
    /// </summary>
    /// <param name="id"> Id of the item in the table </param>
    /// <param name="ct"> Cancellation token </param>
    /// <returns> Found entity by the id, or null if there is no such item</returns>
    public Task<TEntity?> GetByIdAsync(Guid id, CancellationToken ct = default);

    /// <summary>
    ///     Gets certain number of entities starting from provided index.
    ///     This method can be used for pagination
    /// </summary>
    /// <param name="from"> Starting index of the entities list </param>
    /// <param name="count"> Number of items recieved by current request </param>
    /// <param name="ct"> Cancellation token </param>
    /// <returns> List of items starting from current index </returns>
    public Task<IList<TEntity>> GetNumberAsync(int from = 0, int count = 0,
        CancellationToken ct = default);

    /// <summary>
    ///     Simple search through table to find all entities that satisfy predicate
    /// </summary>
    /// <param name="predicate"> Predicate to search for entities </param>
    /// <param name="ct"> Cancellation token </param>
    /// <returns> List of items found by predicate </returns>
    public Task<IList<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken ct = default);
}