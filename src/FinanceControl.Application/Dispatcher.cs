using System.Reflection;
using FinanceControl.Application.Abstractions;
using FinanceControl.Application.Common;
using FinanceControl.Application.Validation;
using FinanceControl.Domain.Events;
using FinanceControl.Domain.Exceptions;
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

    public async Task<Result<TResult>> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
    {
        var fluentResult = await FluentValidationPipeline.ValidateAsync(serviceProvider, query, cancellationToken);
        if (fluentResult is { IsFailure: true })
            return Result<TResult>.Failure(fluentResult.Error!);

        var queryType = query.GetType();
        var handlerType = typeof(IQueryHandler<,>).MakeGenericType(queryType, typeof(TResult));
        var handler = serviceProvider.GetRequiredService(handlerType);
        
        var handleMethod = handlerType.GetMethod(nameof(IQueryHandler<IQuery<TResult>, TResult>.HandleAsync));

        try {
            var handlerResult = handleMethod!.Invoke(handler, new object[] { query, cancellationToken });
            return await (Task<Result<TResult>>)handlerResult!;
        } catch (Exception) {
            return Result<TResult>.Failure(Error.Unexpected($"Ocorreu um erro inesperado"));
        }
    }

    public async Task<Result<TResult>> SendAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default)
    {
        var fluentResult = await FluentValidationPipeline.ValidateAsync(serviceProvider, command, cancellationToken);
        if (fluentResult is { IsFailure: true })
            return Result<TResult>.Failure(fluentResult.Error!);

        var commandType = command.GetType();
        var handlerType = typeof(ICommandHandler<,>).MakeGenericType(commandType, typeof(TResult));
        var handler = serviceProvider.GetRequiredService(handlerType);
        
        var handleMethod = handlerType.GetMethod(nameof(ICommandHandler<ICommand<TResult>, TResult>.HandleAsync));

        try {
            var handlerResult = handleMethod!.Invoke(handler, new object[] { command, cancellationToken });
            return await (Task<Result<TResult>>)handlerResult!;
        } catch (DomainException ex) {
            return Result<TResult>.Failure(Error.Validation(ex.Message));
        } catch (TargetInvocationException tie) when (tie.InnerException is DomainException de) {
            return Result<TResult>.Failure(Error.Validation(de.Message));
        } catch (Exception) {
            return Result<TResult>.Failure(Error.Unexpected($"Ocorreu um erro inesperado"));
        }
    }

    public async Task<Result> SendAsync(ICommand command, CancellationToken cancellationToken = default)
    {
        var fluentResult = await FluentValidationPipeline.ValidateAsync(serviceProvider, command, cancellationToken);
        if (fluentResult is { IsFailure: true })
            return Result.Failure(fluentResult.Error!);

        var commandType = command.GetType();
        var handlerType = typeof(ICommandHandler<>).MakeGenericType(commandType);
        var handler = serviceProvider.GetRequiredService(handlerType);
        
        var handleMethod = handlerType.GetMethod(nameof(ICommandHandler<ICommand>.HandleAsync));

        try {
            var handlerResult = handleMethod!.Invoke(handler, new object[] { command, cancellationToken });
            return await (Task<Result>)handlerResult!;
        } catch (DomainException ex) {
            return Result.Failure(Error.Validation(ex.Message));
        } catch (TargetInvocationException tie) when (tie.InnerException is DomainException de) {
            return Result.Failure(Error.Validation(de.Message));
        } catch (Exception) {
            return Result.Failure(Error.Unexpected($"Ocorreu um erro inesperado"));
        }
    }
}
