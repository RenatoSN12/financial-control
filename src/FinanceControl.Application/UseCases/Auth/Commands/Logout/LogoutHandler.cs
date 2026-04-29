using FinanceControl.Application.Abstractions;
using FinanceControl.Application.Common;
using FinanceControl.Application.UseCases.Abstractions;
using FinanceControl.Domain.Repositories;

namespace FinanceControl.Application.UseCases.Auth.Commands.Logout;

public class LogoutHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    ICurrentUser currentUser
) : ICommandHandler<LogoutCommand, LogoutResponse>
{
    public async Task<Result<LogoutResponse>> HandleAsync(
        LogoutCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var userId = currentUser.UserId;
        
        var user = await userRepository.GetByIdAsync(userId, cancellationToken) 
            ?? throw new InvalidOperationException($"Usuário autenticado com ID {userId} não encontrado.");
            
        user.RevokeRefreshToken();

        await unitOfWork.CommitAsync(cancellationToken);

        return Result<LogoutResponse>.Success(new LogoutResponse());
    }
}