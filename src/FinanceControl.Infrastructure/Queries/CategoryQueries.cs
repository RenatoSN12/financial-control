using FinanceControl.Application.Interfaces;
using FinanceControl.Application.UseCases.Categories.Queries.GetAllCategoriesByUser;
using FinanceControl.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FinanceControl.Infrastructure.Queries;

public class CategoryQueries(AppDbContext context) : ICategoryQueries
{
    public async Task<IReadOnlyCollection<GetAllCategoriesByUserResponse>> GetAllByUserAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
        => await context.Categories
            .AsNoTracking()
            .Where(c => c.UserId == userId)
            .OrderBy(c => c.Name)
            .Select(c => new GetAllCategoriesByUserResponse(c.Id, c.Name))
            .ToListAsync(cancellationToken);
}
