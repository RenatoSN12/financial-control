using FinanceControl.Domain.Entities;
using FinanceControl.Domain.Repositories;
using FinanceControl.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FinanceControl.Infrastructure.Repositories;

public class CategoryRepository(AppDbContext context)
    : BaseRepository<Category>(context), ICategoryRepository
{
    public async Task<bool> ExistsByNameAndUserIdAsync(
        string name,
        Guid userId,
        CancellationToken cancellationToken
    )
        => await Context.Categories
            .AnyAsync(
                c => c.Name == name 
                && c.UserId == userId,
                cancellationToken
            );
}
