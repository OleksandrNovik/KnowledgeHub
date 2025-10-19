namespace KnowledgeHub.Application.Repositories.Primitives;

/// <summary>
///     Interface for db transaction that is used in repositories
/// </summary>
public interface IRepositoryTransaction : IAsyncDisposable
{
    /// <summary>
    ///     Commits the current transaction asynchronously.
    /// </summary>
    public ValueTask CommitAsync(CancellationToken ct);

    /// <summary>
    ///     Asynchronously rolls back the current transaction.
    /// </summary>
    public ValueTask RollbackAsync(CancellationToken ct);

    /// <summary>
    ///     Ensures transaction is completed
    /// </summary>
    /// <remarks> If transaction is not closed when calling this method - it is rolled back by default</remarks>
    public ValueTask EnsureClosedAsync(CancellationToken ct = default);
}