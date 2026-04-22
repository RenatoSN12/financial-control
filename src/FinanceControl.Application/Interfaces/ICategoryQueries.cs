using FinanceControl.Application.UseCases.Categories.Queries.GetAllCategoriesByUser;

namespace FinanceControl.Application.Interfaces;

public interface ICategoryQueries
{
    Task<IReadOnlyCollection<GetAllCategoriesByUserResponse>> GetAllByUserAsync(Guid userId, CancellationToken cancellationToken = default);
}
