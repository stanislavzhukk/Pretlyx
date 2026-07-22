namespace Application.Abstractions.Data;

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

public interface IAppDbContext
{
    DbSet<TodoItem> Todos { get; }
    DbSet<OwnerProfile> OwnerProfiles { get; }
    DbSet<WalkerProfile> WalkerProfiles { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
