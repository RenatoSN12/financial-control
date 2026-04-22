using FinanceControl.Application.Abstractions;

namespace FinanceControl.Application.UseCases.Categories.Commands.CreateCategory;

public record CreateCategoryCommand(
    string Name
) : ICommand<CreateCategoryResponse>;