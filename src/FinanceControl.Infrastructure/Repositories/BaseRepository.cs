using FinanceControl.Domain.Entities;
using FinanceControl.Domain.Repositories;
using FinanceControl.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FinanceControl.Infrastructure.Repositories;

public abstract class BaseRepository<T>(AppDbContext context) : IWriteRepository<T>
    where T : AggregateRoot
{
    protected readonly AppDbContext Context = context;
    private DbSet<T> DbSet => Context.Set<T>();

    public async Task<T?> FindAsync(
        Guid id,
        CancellationToken cancellationToken = default
    )
        => await DbSet.FirstOrDefaultAsync(
            e => e.Id == id,
            cancellationToken
        );

    public async Task<bool> ExistsAsync(
        Guid id,
        CancellationToken cancellationToken = default
    )
        => await DbSet.AnyAsync(
            e => e.Id == id, 
            cancellationToken
        );

    public async Task AddAsync(
        T entity,
        CancellationToken cancellationToken = default
    )
        => await DbSet.AddAsync(
            entity,
            cancellationToken
        );

    public void Update(T entity)
        => Context.Update(entity);

    public void Delete(T entity)
        => DbSet.Remove(entity);
}
