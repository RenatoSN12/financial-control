using FinanceControl.Domain.Events;

namespace FinanceControl.Application.Abstractions;

public interface IEventHandler<TDomainEvent> where TDomainEvent : IDomainEvent
{
    Task HandleAsync(TDomainEvent domainEvent, CancellationToken cancellationToken = default);
}