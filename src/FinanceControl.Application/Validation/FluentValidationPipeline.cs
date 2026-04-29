using System.Reflection;
using System.Windows.Input;
using FinanceControl.Application.Abstractions;
using FinanceControl.Application.Common;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceControl.Application.Validation;

internal static class FluentValidationPipeline
{
    public static async Task<Result?> ValidateAsync<TRequest>(
        IServiceProvider serviceProvider,
        TRequest request,
        CancellationToken cancellationToken)
    {
        var validator = serviceProvider.GetService<IValidator<TRequest>>();
        if (validator is null)
            return null;

        var validationResult = await validator.ValidateAsync(
            request,
            cancellationToken
        );

        if (validationResult.IsValid)
            return Result.Success();

        var message = string.Join(
            "; ",
            validationResult.Errors.Select(e => e.ErrorMessage)
        );

        return Result.Failure(Error.Validation(message));
    }
}
