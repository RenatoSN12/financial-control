namespace FinanceControl.Application.UseCases.Categories.Queries.GetAllCategoriesByUser;

public record GetAllCategoriesByUserResponse(
    Guid Id,
    string Name
);