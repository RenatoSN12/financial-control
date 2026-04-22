using FinanceControl.Domain.Entities;

namespace FinanceControl.Domain.Repositories;

public interface ICategoryRepository : IWriteRepository<Category>
{
    Task<bool> ExistsByNameAndUserIdAsync(
        string name,
        Guid userId,
        CancellationToken cancellationToken
    );
}