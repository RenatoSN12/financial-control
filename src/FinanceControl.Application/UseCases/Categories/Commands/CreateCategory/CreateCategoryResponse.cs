namespace FinanceControl.Application.UseCases.Categories.Commands.CreateCategory;

public record CreateCategoryResponse(
    Guid Id,
    string Name
);