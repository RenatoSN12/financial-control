using FinanceControl.Application.Abstractions;

namespace FinanceControl.Application.UseCases.Categories.Queries.GetAllCategoriesByUser;

public record GetAllCategoriesByUserQuery(
    Guid UserId
) : IQuery<IReadOnlyCollection<GetAllCategoriesByUserResponse>>;
