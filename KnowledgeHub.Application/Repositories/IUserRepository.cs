using KnowledgeHub.Application.Repositories.Generic;
using KnowledgeHub.Domain.Entities.User;

namespace KnowledgeHub.Application.Repositories;

/// <summary>
///     Interface for repository of <see cref="UserEntity" />
/// </summary>
public interface IUserRepository : IGenericRepository<UserEntity>
{
    /// <summary>
    ///     Gets user by email.
    /// </summary>
    /// <param name="email"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public Task<UserEntity?> GetUserByEmailAsync(string email, CancellationToken ct = default);
}