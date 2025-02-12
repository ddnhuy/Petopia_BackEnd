using System.Linq.Expressions;
using Domain.Auths;
using Domain.PetAlerts;
using Domain.Pets;
using Domain.Todos;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<ApplicationUser> Users { get; }

    DbSet<TodoItem> TodoItems { get; }

    DbSet<RefreshToken> RefreshTokens { get; }

    DbSet<Pet> Pets { get; }
    DbSet<PetWeight> PetWeights { get; }
    DbSet<PetVaccination> PetVaccinations { get; }

    DbSet<PetAlert> PetAlerts { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<int> ExecuteDeleteAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) where TEntity : class;
}
