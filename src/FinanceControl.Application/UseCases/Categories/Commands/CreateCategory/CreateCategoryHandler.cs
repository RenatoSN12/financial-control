using FinanceControl.Application.Abstractions;
using FinanceControl.Application.Common;
using FinanceControl.Application.UseCases.Abstractions;
using FinanceControl.Domain.Entities;
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
        var userId = currentUser.UserId;

        var nameAlreadyInUse = await categoryRepository.ExistsByNameAndUserIdAsync(
            command.Name,
            userId,
            cancellationToken
        );

        if (nameAlreadyInUse)
            return Result<CreateCategoryResponse>.Failure(
                Error.Validation("Já existe uma categoria com este nome")
            );

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
}
