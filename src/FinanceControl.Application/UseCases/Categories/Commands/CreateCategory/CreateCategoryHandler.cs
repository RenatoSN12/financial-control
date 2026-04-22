using FinanceControl.Application.Abstractions;
using FinanceControl.Application.Common;
using FinanceControl.Application.UseCases.Abstractions;
using FinanceControl.Domain.Entities;
using FinanceControl.Domain.Exceptions;
using FinanceControl.Domain.Repositories;

namespace FinanceControl.Application.UseCases.Categories.Commands.CreateCategory;

public class CreateCategoryHandler(
    ICategoryRepository categoryRepository,
    ICurrentUser currentUser,
    IUnitOfWork unitOfWork
) : ICommandHandler<CreateCategoryCommand, CreateCategoryResponse>
{
    public async Task<Result<CreateCategoryResponse>> HandleAsync(
        CreateCategoryCommand command,
        CancellationToken cancellationToken = default
    )
    {
        try 
        {
            var userId = currentUser.UserId;

            var validationResult = await ValidateAsync(
                command,
                userId,
                cancellationToken
            );

            if (validationResult.IsFailure)
                return Result<CreateCategoryResponse>.Failure(validationResult.Error!);

            var category = Category.Create(userId, command.Name);

            await categoryRepository.AddAsync(
                category,
                cancellationToken
            );

            await unitOfWork.CommitAsync(cancellationToken);

            var response = new CreateCategoryResponse(
                category.Id,
                category.Name
            );

            return Result<CreateCategoryResponse>.Success(response);
        }
        catch (DomainException ex) 
        {
            var error = Error.Validation(ex.Message);
            return Result<CreateCategoryResponse>.Failure(error);
        }
        catch (Exception ex)
        {
            var error = Error.Unexpected(ex.Message);
            return Result<CreateCategoryResponse>.Failure(error);
        }
    }

    private async Task<Result> ValidateAsync(
        CreateCategoryCommand command,
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        var validator = new CreateCategoryCommandValidator();
        
        var validationResult = await validator.ValidateAsync(
            command,
            cancellationToken
        );

        if (!validationResult.IsValid)
        {
            var error = Error.Validation(validationResult.Errors.First().ErrorMessage);
            return Result.Failure(error);
        }

        var categoryExists = await categoryRepository.ExistsByNameAndUserIdAsync(
            command.Name,
            userId,
            cancellationToken
        );

        if (categoryExists)
        {
            var error = Error.Validation("Já existe uma categoria com este nome");
            return Result.Failure(error);
        }

        return Result.Success();
    }
}