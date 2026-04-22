using FinanceControl.Application.Abstractions;
using FinanceControl.Application.Common;
using FinanceControl.Domain.Events;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceControl.Application;

public class Dispatcher(
    IServiceProvider serviceProvider
) : IDispatcher
{
    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) 
        where TEvent : IDomainEvent
    {
        var handlers = serviceProvider.GetServices<IEventHandler<TEvent>>().ToArray();
        foreach(var handler in handlers)
            await handler.HandleAsync(@event, cancellationToken);
    }

    public Task<Result<TResult>> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
    {
        var queryType = query.GetType();
        var handlerType = typeof(IQueryHandler<,>).MakeGenericType(queryType, typeof(TResult));
        var handler = serviceProvider.GetRequiredService(handlerType);
        
        var handleMethod = handlerType.GetMethod(nameof(IQueryHandler<IQuery<TResult>, TResult>.HandleAsync));
        var result = handleMethod!.Invoke(handler, new object[] { query, cancellationToken });
        
        return (Task<Result<TResult>>)result!;
    }

    public Task<Result<TResult>> SendAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default)
    {
        var commandType = command.GetType();
        var handlerType = typeof(ICommandHandler<,>).MakeGenericType(commandType, typeof(TResult));
        var handler = serviceProvider.GetRequiredService(handlerType);
        
        var handleMethod = handlerType.GetMethod(nameof(ICommandHandler<ICommand<TResult>, TResult>.HandleAsync));
        var result = handleMethod!.Invoke(handler, new object[] { command, cancellationToken });
        
        return (Task<Result<TResult>>)result!;
    }

    public Task<Result> SendAsync(ICommand command, CancellationToken cancellationToken = default)
    {
        var commandType = command.GetType();
        var handlerType = typeof(ICommandHandler<>).MakeGenericType(commandType);
        var handler = serviceProvider.GetRequiredService(handlerType);
        
        var handleMethod = handlerType.GetMethod(nameof(ICommandHandler<ICommand>.HandleAsync));
        var result = handleMethod!.Invoke(handler, new object[] { command, cancellationToken });
        
        return (Task<Result>)result!;
    }
}
