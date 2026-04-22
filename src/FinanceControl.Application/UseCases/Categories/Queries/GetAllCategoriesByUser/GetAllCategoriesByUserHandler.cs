using FinanceControl.Application.Abstractions;
using FinanceControl.Application.Common;
using FinanceControl.Application.Interfaces;

namespace FinanceControl.Application.UseCases.Categories.Queries.GetAllCategoriesByUser;

public class GetAllCategoriesByUserHandler(
    ICategoryQueries categoryQueries
) : IQueryHandler<GetAllCategoriesByUserQuery, IReadOnlyCollection<GetAllCategoriesByUserResponse>>
{
    public async Task<Result<IReadOnlyCollection<GetAllCategoriesByUserResponse>>> HandleAsync(
        GetAllCategoriesByUserQuery query,
        CancellationToken cancellationToken = default)
    {
        var categories = await categoryQueries.GetAllByUserAsync(
            query.UserId,
            cancellationToken
        );

        return Result<IReadOnlyCollection<GetAllCategoriesByUserResponse>>.Success(categories);
    }
}
