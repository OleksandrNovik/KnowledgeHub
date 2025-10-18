using KnowledgeHub.Application.Repositories.Generic;
using KnowledgeHub.Domain.Entities.User;

namespace KnowledgeHub.Application.Repositories;

/// <summary>
///     Interface for repository of <see cref="UserEntity" />
/// </summary>
public interface IUserRepository : IGenericRepository<UserEntity>;