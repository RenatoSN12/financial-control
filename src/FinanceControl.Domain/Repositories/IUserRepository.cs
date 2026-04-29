using FinanceControl.Domain.Entities;

namespace FinanceControl.Domain.Repositories;

public interface IUserRepository : IWriteRepository<User>
{
    Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<User?> GetByRefreshTokenHashAsync(string refreshTokenHash, CancellationToken cancellationToken);
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}