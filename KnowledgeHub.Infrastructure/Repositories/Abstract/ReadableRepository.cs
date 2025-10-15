using System.Linq.Expressions;
using KnowledgeHub.Application.Repositories.Generic;
using KnowledgeHub.Domain.Entities.Abstract;
using KnowledgeHub.Infrastructure.Repositories.Enum;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeHub.Infrastructure.Repositories.Abstract;

/// <summary>
///     Base implementation of read operations in repository
/// </summary>
/// <typeparam name="TEntity"> Type of entity for repository </typeparam>
public abstract class ReadableRepository<TEntity>(DbContext dbContext)
    : IReadableRepository<TEntity> where TEntity : BaseEntity
{
    /// <summary>
    ///     Gets general data about all entities from database. Does not include any additional joins.
    ///     By default will not track any changes for retrieved entities
    /// </summary>
    public virtual async Task<IList<TEntity>> GetAllAsync(CancellationToken ct = default)
    {
        return await QueryAll(TrackingBehavior.NoTracking).ToListAsync(ct);
    }

    /// <summary>
    ///     Gets first item by provided predicate. By default will not track any changes in the entity
    /// </summary>
    public virtual async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken ct = default)
    {
        return await QueryAll(TrackingBehavior.NoTracking).FirstOrDefaultAsync(predicate, ct);
    }

    /// <summary>
    ///     Gets first item by id. By default will not track any changes in the entity
    /// </summary>
    public Task<TEntity?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return FirstOrDefaultAsync(e => e.Id == id, ct);
    }

    /// <summary>
    ///     Takes number of entities from database starting from provided index. Does not include any additional joins.
    ///     By default will not track any changes for retrieved entities
    /// </summary>
    public virtual async Task<IList<TEntity>> GetNumberAsync(int from = 0, int count = 0,
        CancellationToken ct = default)
    {
        var queryable = QueryAll(TrackingBehavior.NoTracking);

        if (from > 0)
        {
            queryable = queryable.Skip(from);
        }

        if (count > 0)
        {
            queryable = queryable.Take(count);
        }

        return await queryable.ToListAsync(ct);
    }

    /// <summary>
    ///     Searches for entities in the table by provided predicate. Does not include any additional joins.
    ///     By default will not track any changes for retrieved entities
    /// </summary>
    public async Task<IList<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken ct = default)
    {
        return await QueryAll(TrackingBehavior.NoTracking).Where(predicate).ToListAsync(ct);
    }

    /// <summary>
    ///     Gets all entities from
    /// </summary>
    /// <param name="trackingBehavior"> Tracking behavior for entities </param>
    protected IQueryable<TEntity> QueryAll(TrackingBehavior trackingBehavior)
    {
        IQueryable<TEntity> queryable = dbContext.Set<TEntity>();

        if (trackingBehavior == TrackingBehavior.NoTracking)
        {
            queryable = queryable.AsNoTracking();
        }

        return queryable;
    }
}