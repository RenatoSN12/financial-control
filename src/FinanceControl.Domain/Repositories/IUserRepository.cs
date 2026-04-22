using FinanceControl.Domain.Entities;

namespace FinanceControl.Domain.Repositories;

public interface IUserRepository : IWriteRepository<User>
{
    Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken);
}