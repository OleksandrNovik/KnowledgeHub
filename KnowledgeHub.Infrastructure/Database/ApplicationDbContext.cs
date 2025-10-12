using KnowledgeHub.Domain.Entities.Knowledge;
using KnowledgeHub.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeHub.Infrastructure.Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        // see https://github.com/npgsql/doc/blob/main/conceptual/Npgsql/types/datetime.md/
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }

    public DbSet<UserEntity> Users { get; set; }

    public DbSet<CategoryEntity> Categories { get; set; }

    /// <summary>
    ///     Many-to-many linking table between <see cref="Categories" /> and <see cref="Knowledge" />
    /// </summary>
    public DbSet<KnowledgeCategoryEntity> KnowledgeCategories { get; set; }

    public DbSet<KnowledgeEntity> Knowledge { get; set; }

    /// <summary>
    ///     Many-to-many linking table between <see cref="Sources" /> and <see cref="Knowledge" />
    /// </summary>
    public DbSet<KnowledgeSourceEntity> KnowledgeSources { get; set; }

    public DbSet<SourceEntity> Sources { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Category can be assigned to many knowledge items
        modelBuilder.Entity<CategoryEntity>()
            .HasMany(c => c.KnowledgeCategories)
            .WithOne(kc => kc.Category)
            .HasForeignKey(kc => kc.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        // Knowledge item can have multiple categories
        modelBuilder.Entity<KnowledgeEntity>()
            .HasMany(k => k.KnowledgeCategories)
            .WithOne(kc => kc.Knowledge)
            .HasForeignKey(kc => kc.KnowledgeId)
            .OnDelete(DeleteBehavior.Cascade);

        // User has many knowledge items that assigned only to one user
        modelBuilder.Entity<KnowledgeEntity>()
            .HasOne(k => k.User)
            .WithMany(u => u.KnowledgeItems)
            .HasForeignKey(k => k.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Knowledge item can have multiple sources
        modelBuilder.Entity<KnowledgeEntity>()
            .HasMany(k => k.KnowledgeSources)
            .WithOne(ks => ks.Knowledge)
            .HasForeignKey(ks => ks.KnowledgeId)
            .OnDelete(DeleteBehavior.Cascade);

        // Source can ba assigned to multiple knowledge items
        modelBuilder.Entity<SourceEntity>()
            .HasMany(k => k.KnowledgeSources)
            .WithOne(ks => ks.Source)
            .HasForeignKey(ks => ks.KnowledgeSourceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}