using FinanceControl.Application.Abstractions;
using FinanceControl.Domain.Entities;
using FinanceControl.Domain.Repositories;
using FinanceControl.Infrastructure.Data;

namespace FinanceControl.Infrastructure.Repositories;

public class UnitOfWork(AppDbContext context, IDispatcher dispatcher) : IUnitOfWork
{
    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        var result = await context.SaveChangesAsync(cancellationToken);
        
        var aggregates = context.ChangeTracker
            .Entries<AggregateRoot>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity)
            .ToList();

        var domainEvents = aggregates
            .SelectMany(a => a.DomainEvents)
            .ToList();
        
        foreach (var aggregate in aggregates)
            aggregate.ClearDomainEvents();
        
        foreach (var domainEvent in domainEvents)
            await dispatcher.PublishAsync(
                (dynamic)domainEvent,
                cancellationToken
            );
        
        return result;
    }
}
