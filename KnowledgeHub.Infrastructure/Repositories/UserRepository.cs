using System.Linq.Expressions;
using KnowledgeHub.Application.Repositories;
using KnowledgeHub.Domain.Entities.User;
using KnowledgeHub.Infrastructure.Database;
using KnowledgeHub.Infrastructure.Repositories.Abstract;
using KnowledgeHub.Infrastructure.Repositories.Enum;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeHub.Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext dbContext)
    : UpdatableRepository<UserEntity>(dbContext), IUserRepository
{
    /// <summary>
    ///     Gets first user by provided predicate. Tracks changes at user entity
    /// </summary>
    public override Task<UserEntity?> FirstOrDefaultAsync(Expression<Func<UserEntity, bool>> predicate,
        CancellationToken ct = default)
    {
        return QueryAll(TrackingBehavior.Track).FirstOrDefaultAsync(predicate, ct);
    }
}