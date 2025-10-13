using Bogus;
using KnowledgeHub.Domain.Dtos.User;
using KnowledgeHub.Domain.Entities.Abstract;
using KnowledgeHub.Domain.Entities.Knowledge;
using KnowledgeHub.Domain.Entities.User;

namespace KnowledgeHub.Domain.Entities;

/// <summary>
///     Class generator for seeding database values for each enitity
/// </summary>
public static class EntityGenerator
{
    /// <summary>
    ///     Generates number of entities of type <see cref="KnowledgeEntity" />.
    ///     NOTE: does not define any relations with other tables
    /// </summary>
    /// <param name="count"> Number of items generated </param>
    /// <returns> Collection if <see cref="KnowledgeEntity" /> items that were generated </returns>
    public static IReadOnlyCollection<KnowledgeEntity> GetKnowledgeItems(int count)
    {
        var faker = GeneralFakerFor<KnowledgeEntity>()
            .RuleFor(k => k.Title, f => f.Lorem.Sentence(range: 3))
            .RuleFor(k => k.Description, f => f.Lorem.Paragraphs(1, 2));

        return faker.Generate(count);
    }

    /// <summary>
    ///     Generates number of entities of type <see cref="CategoryEntity" />.
    ///     NOTE: does not define any relations with other tables
    /// </summary>
    /// <param name="count"> Number of items generated </param>
    /// <returns> Collection if <see cref="CategoryEntity" /> items that were generated </returns>
    public static IReadOnlyCollection<CategoryEntity> GetCategories(int count)
    {
        var faker = GeneralFakerFor<CategoryEntity>()
            .RuleFor(c => c.Name, f => f.Lorem.Word());

        return faker.Generate(count);
    }

    /// <summary>
    ///     Generates number of entities of type <see cref="UserEntity" />.
    ///     NOTE: does not define any relations with other tables
    /// </summary>
    /// <param name="count"> Number of items generated </param>
    /// <returns> Collection if <see cref="UserEntity" /> items that were generated </returns>
    public static IReadOnlyCollection<UserEntity> GetUsers(int count)
    {
        var faker = GeneralFakerFor<UserEntity>()
            .RuleFor(u => u.Username, f => f.Person.UserName)
            .RuleFor(u => u.Email, f => f.Person.Email)
            .RuleFor(u => u.Password, f => f.Hashids.Encode(f.Random.Digits(4)))
            .RuleFor(u => u.Role, f => UserRole.Default);

        return faker.Generate(count);
    }

    /// <summary>
    ///     Generates number of entities of type <see cref="SourceEntity" />.
    ///     NOTE: does not define any relations with other tables
    /// </summary>
    /// <param name="count"> Number of items generated </param>
    /// <returns> Collection if <see cref="SourceEntity" /> items that were generated </returns>
    public static IReadOnlyCollection<SourceEntity> GetSources(int count)
    {
        var faker = GeneralFakerFor<SourceEntity>()
            .RuleFor(s => s.Url, f => f.Internet.Url())
            .RuleFor(s => s.Name, f => f.Lorem.Sentence(range: 1));

        return faker.Generate(count);
    }

    /// <summary>
    ///     Generates simple relations from table <see cref="KnowledgeEntity" /> to <see cref="SourceEntity" />
    /// </summary>
    /// <returns> Collection of linking entitites </returns>
    public static IReadOnlyCollection<KnowledgeSourceEntity> LinkSources(
        IReadOnlyCollection<KnowledgeEntity> knowledgeItems, IReadOnlyCollection<SourceEntity> sources)
    {
        var result = new List<KnowledgeSourceEntity>();
        var faker = new Faker();
        foreach (var knowledgeSource in knowledgeItems)
        {
            var sourceEntity = faker.PickRandom<SourceEntity>(sources);
            var sourceLink = new KnowledgeSourceEntity
            {
                KnowledgeId = knowledgeSource.Id,
                Knowledge = knowledgeSource,
                Source = sourceEntity,
                KnowledgeSourceId = sourceEntity.Id,
                CreationTime = faker.Date.Recent()
            };

            result.Add(sourceLink);
        }

        return result;
    }

    /// <summary>
    ///     Generates simple relations from table <see cref="KnowledgeEntity" /> to <see cref="CategoryEntity" />
    /// </summary>
    /// <returns> Collection of linking entitites </returns>
    public static IReadOnlyCollection<KnowledgeCategoryEntity> LinkCategories(
        IReadOnlyCollection<KnowledgeEntity> knowledgeItems, IReadOnlyCollection<CategoryEntity> categories)
    {
        var result = new List<KnowledgeCategoryEntity>();
        var faker = new Faker();
        foreach (var knowledgeItem in knowledgeItems)
        {
            var selectedCategories = faker.PickRandom(categories, faker.Random.Number(1, 3));

            foreach (var category in selectedCategories)
            {
                var categorySource = new KnowledgeCategoryEntity
                {
                    Category = category,
                    CategoryId = category.Id,
                    KnowledgeId = knowledgeItem.Id,
                    Knowledge = knowledgeItem
                };

                result.Add(categorySource);
            }
        }

        return result;
    }

    /// <summary>
    ///     Generates simple relations from table <see cref="KnowledgeEntity" /> to <see cref="UserEntity" />
    /// </summary>
    /// <returns> Collection of linking entitites </returns>
    public static void LinkUsers(IReadOnlyCollection<KnowledgeEntity> knowledgeItems,
        IReadOnlyCollection<UserEntity> users)
    {
        var faker = new Faker();
        foreach (var knowledgeEntity in knowledgeItems)
        {
            var user = faker.PickRandom<UserEntity>(users);
            knowledgeEntity.User = user;
            knowledgeEntity.UserId = user.Id;
            user.KnowledgeItems.Add(knowledgeEntity);
        }
    }

    /// <summary>
    ///     Creates default faker for <see cref="BaseEntity" /> that will generate random id, creation date and modification
    ///     date
    /// </summary>
    /// <typeparam name="T"> Type of entity that faker is created for </typeparam>
    /// <returns> Faker of entity of provided type</returns>
    private static Faker<T> GeneralFakerFor<T>()
        where T : BaseEntity
    {
        return new Faker<T>()
            .RuleFor(e => e.Id, f => Guid.NewGuid())
            .RuleFor(e => e.CreationTime, f => f.Date.Recent().ToUniversalTime())
            .RuleFor(e => e.LastModificationTime, f => DateTime.Now.ToUniversalTime());
    }
}