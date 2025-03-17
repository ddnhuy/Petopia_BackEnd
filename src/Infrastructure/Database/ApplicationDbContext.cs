using System.Data;
using System.Linq.Expressions;
using Application.Abstractions.Data;
using Domain.Advertisement;
using Domain.Auths;
using Domain.Comments;
using Domain.Notifications;
using Domain.PetAlerts;
using Domain.Pets;
using Domain.Posts;
using Domain.Reactions;
using Domain.Todos;
using Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using SharedKernel;

namespace Infrastructure.Database;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IPublisher publisher)
    : IdentityDbContext<ApplicationUser>(options), IApplicationDbContext
{
    public DbSet<TodoItem> TodoItems { get; set; }

    public DbSet<RefreshToken> RefreshTokens { get; set; }

    public DbSet<Pet> Pets { get; set; }
    public DbSet<PetWeight> PetWeights { get; set; }
    public DbSet<PetVaccination> PetVaccinations { get; set; }

    public DbSet<PetAlert> PetAlerts { get; set; }

    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Reaction> Reactions { get; set; }

    public DbSet<Notification> Notifications { get; set; }
    public DbSet<NotificationSubscription> NotificationSubscriptions { get; set; }

    public DbSet<Ad> Ads { get; set; }
    public DbSet<AdEvent> AdEvents { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        foreach (IMutableEntityType entityType in builder.Model.GetEntityTypes())
        {
            string? tableName = entityType.GetTableName();
            if (!string.IsNullOrEmpty(tableName) && tableName.StartsWith("AspNet", StringComparison.CurrentCulture))
            {
                entityType.SetTableName(tableName.Substring(6));
            }
        }

        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        builder.HasDefaultSchema(Schemas.Default);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // When should you publish domain events?
        //
        // 1. BEFORE calling SaveChangesAsync
        //     - domain events are part of the same transaction
        //     - immediate consistency
        // 2. AFTER calling SaveChangesAsync
        //     - domain events are a separate transaction
        //     - eventual consistency
        //     - handlers can fail

        SetUpdatedAt();

        int result = await base.SaveChangesAsync(cancellationToken);

        await PublishDomainEventsAsync();

        return result;
    }

    public async Task<int> ExecuteDeleteAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) where TEntity : class
    {
        return await Set<TEntity>().Where(predicate).ExecuteDeleteAsync(cancellationToken);
    }

    private async Task PublishDomainEventsAsync()
    {
        var domainEvents = ChangeTracker
            .Entries<Entity>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                List<IDomainEvent> domainEvents = entity.DomainEvents;

                entity.ClearDomainEvents();

                return domainEvents;
            })
            .ToList();

        foreach (IDomainEvent domainEvent in domainEvents)
        {
            await publisher.Publish(domainEvent);
        }
    }

    private void SetUpdatedAt()
    {
        IEnumerable<EntityEntry> entries = ChangeTracker.Entries()
            .Where(e => e.Entity is Entity && (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (EntityEntry entry in entries)
        {
            var entity = (Entity)entry.Entity;
            entity.UpdatedAt = DateTime.UtcNow;

            if (entry.State == EntityState.Added)
            {
                entity.CreatedAt = DateTime.UtcNow;
            }
        }
    }
}
