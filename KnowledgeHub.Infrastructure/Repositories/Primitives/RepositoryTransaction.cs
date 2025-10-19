using KnowledgeHub.Application.Repositories.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace KnowledgeHub.Infrastructure.Repositories.Primitives;

/// <summary>
///     Wrapper on <see cref="IDbContextTransaction" /> that is used to execute transactions on database update operations
/// </summary>
/// <param name="dbTransaction"> Instance of transaction returned from <see cref="DbContext" /> </param>
public struct RepositoryTransaction(IDbContextTransaction dbTransaction) : IRepositoryTransaction
{
    private bool isClosed = false;

    /// <inheritdoc />
    public async ValueTask DisposeAsync()
    {
        await EnsureClosedAsync();
    }

    /// <inheritdoc />
    public async ValueTask CommitAsync(CancellationToken ct)
    {
        try
        {
            await dbTransaction.CommitAsync(ct);
            isClosed = true;
        }
        catch (Exception ex)
        {
            isClosed = false;
        }
    }

    /// <inheritdoc />
    public async ValueTask RollbackAsync(CancellationToken ct)
    {
        await dbTransaction.RollbackAsync(ct);
    }

    /// <inheritdoc />
    public async ValueTask EnsureClosedAsync(CancellationToken ct = default)
    {
        if (!isClosed)
        {
            await RollbackAsync(ct);
        }
    }
}