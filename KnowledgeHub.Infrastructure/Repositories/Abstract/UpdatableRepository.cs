using KnowledgeHub.Application.Repositories.Generic;
using KnowledgeHub.Domain.Entities.Abstract;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeHub.Infrastructure.Repositories.Abstract;

/// <summary>
///     Base implementation of create, update and delete repository operations
/// </summary>
/// <param name="dbContext"> Db context for access to data base </param>
/// <typeparam name="TEntity"> Type of repository entity </typeparam>
public abstract class UpdatableRepository<TEntity>(DbContext dbContext)
    : ReadableRepository<TEntity>(dbContext), IUpdatableRepository<TEntity>
    where TEntity : BaseEntity
{
    /// <inheritdoc />
    public virtual async Task AddAsync(TEntity entity, CancellationToken ct)
    {
        await dbContext.Set<TEntity>().AddAsync(entity, ct);

        await dbContext.SaveChangesAsync(ct);
    }

    /// <inheritdoc />
    public virtual async Task UpdateAsync(TEntity entity, CancellationToken ct)
    {
        dbContext.Set<TEntity>().Update(entity);

        await dbContext.SaveChangesAsync(ct);
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken ct)
    {
        var entity = await GetByIdAsync(id, ct);
        if (entity != null)
        {
            await DeleteAsync(entity, ct);
        }
    }

    /// <inheritdoc />
    public virtual async Task DeleteAsync(TEntity entity, CancellationToken ct)
    {
        dbContext.Set<TEntity>().Remove(entity);

        await dbContext.SaveChangesAsync(ct);
    }
}