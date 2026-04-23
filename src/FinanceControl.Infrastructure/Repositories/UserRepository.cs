using FinanceControl.Domain.Entities;
using FinanceControl.Domain.Repositories;
using FinanceControl.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FinanceControl.Infrastructure.Repositories;

public class UserRepository(AppDbContext context)
    : BaseRepository<User>(context), IUserRepository
{
    public async Task<bool> ExistsByEmailAsync(
        string email,
        CancellationToken cancellationToken
    )
        => await Context.Users
            .AnyAsync(
                u => u.Email.Address == email,
                cancellationToken
            );

    public async Task<User?> GetByEmailAsync(
        string email,
        CancellationToken cancellationToken
    )
        => await Context.Users
            .FirstOrDefaultAsync(
                u => u.Email.Address == email,
                cancellationToken
            );
}
