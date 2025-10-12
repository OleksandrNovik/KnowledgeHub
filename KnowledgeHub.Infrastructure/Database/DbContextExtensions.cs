using KnowledgeHub.Domain.Entities;
using KnowledgeHub.Domain.Entities.Knowledge;
using KnowledgeHub.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeHub.Infrastructure.Database;

/// <summary>
///     Extension methods of <see cref="DbContext" />
/// </summary>
public static class DbContextExtensions
{
    /// <summary>
    ///     Seeds database with random data when it is empty.
    ///     Method is used for development purposes
    /// </summary>
    /// <param name="context"> Context of database that is seeded</param>
    /// <param name="ct"> Cancellation token </param>
    public static async Task SeedAsync(this DbContext context, CancellationToken ct)
    {
        var hasData = await context.Set<KnowledgeEntity>().AnyAsync(ct);

        if (!hasData)
        {
            var knowledgeItems = EntityGenerator.GetKnowledgeItems(10);
            var categories = EntityGenerator.GetCategories(7);
            var sources = EntityGenerator.GetSources(5);
            var users = EntityGenerator.GetUsers(3);
            var sourceLinks = EntityGenerator.LinkSources(knowledgeItems, sources);
            var categoryLinks = EntityGenerator.LinkCategories(knowledgeItems, categories);
            EntityGenerator.LinkUsers(knowledgeItems, users);

            await context.Set<KnowledgeEntity>().AddRangeAsync(knowledgeItems, ct);
            await context.Set<CategoryEntity>().AddRangeAsync(categories, ct);
            await context.Set<SourceEntity>().AddRangeAsync(sources, ct);
            await context.Set<UserEntity>().AddRangeAsync(users, ct);
            await context.Set<KnowledgeSourceEntity>().AddRangeAsync(sourceLinks, ct);
            await context.Set<KnowledgeCategoryEntity>().AddRangeAsync(categoryLinks, ct);

            await context.SaveChangesAsync(ct);
        }
    }
}