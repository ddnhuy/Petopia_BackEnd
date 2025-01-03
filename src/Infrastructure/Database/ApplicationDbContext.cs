using System.Data;
using System.Linq.Expressions;
using Application.Abstractions.Data;
using Domain.Todos;
using Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SharedKernel;

namespace Infrastructure.Database;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IPublisher publisher)
    : IdentityDbContext<ApplicationUser>(options), IApplicationDbContext
{
    public DbSet<TodoItem> TodoItems { get; set; }

    public DbSet<RefreshToken> RefreshTokens { get; set; }

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
}
