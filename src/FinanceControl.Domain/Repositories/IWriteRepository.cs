using FinanceControl.Domain.Entities;

namespace FinanceControl.Domain.Repositories;

public interface IWriteRepository<T> where T : AggregateRoot
{
    Task<T?> FindAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(T entity, CancellationToken cancellationToken = default);
    void Update(T entity);
    void Delete(T entity);
}
