using FinanceControl.Application.Abstractions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceControl.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IDispatcher, Dispatcher>();

        var assembly = typeof(ServiceCollectionExtensions).Assembly;

        foreach (var type in assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract))
        {
            foreach (var @interface in type.GetInterfaces())
            {
                if (!@interface.IsGenericType) continue;

                var definition = @interface.GetGenericTypeDefinition();

                if (definition == typeof(ICommandHandler<,>) ||
                    definition == typeof(ICommandHandler<>)  ||
                    definition == typeof(IQueryHandler<,>)   ||
                    definition == typeof(IEventHandler<>))
                {
                    services.AddScoped(@interface, type);
                }
            }
        }

        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}
