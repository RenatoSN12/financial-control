using FinanceControl.Application.Common;
using FinanceControl.Domain.Events;

namespace FinanceControl.Application.Abstractions;

public interface IDispatcher
{
    Task<Result<TResult>> SendAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default);
    Task<Result> SendAsync(ICommand command, CancellationToken cancellationToken = default);
    Task<Result<TResult>> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default);
    Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
        where TEvent : IDomainEvent;
}