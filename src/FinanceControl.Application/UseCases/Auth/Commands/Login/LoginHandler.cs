using FinanceControl.Application.Abstractions;
using FinanceControl.Application.Common;
using FinanceControl.Domain.Repositories;
using FinanceControl.Domain.Services;

namespace FinanceControl.Application.UseCases.Auth.Commands.Login;

public class LoginHandler(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IJwtService jwtService,
    IUnitOfWork unitOfWork
) : ICommandHandler<LoginCommand, LoginResponse>
{
    public async Task<Result<LoginResponse>> HandleAsync(
        LoginCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var normalizedEmail = command.Email.Trim().ToLowerInvariant();
            
        var user = await userRepository.GetByEmailAsync(normalizedEmail, cancellationToken);
        if (user == null || !user.IsActive)
            return Result<LoginResponse>.Failure(
                Error.NotFound("Email ou senha inválidos.")
            );

        if (!passwordHasher.Verify(command.Password, user.PasswordHash))
             return Result<LoginResponse>.Failure(
                Error.Validation("Email ou senha inválidos.")
            );

        var accessToken = jwtService.GenerateAccessToken(user.Id, user.Email.Address);
        var refreshTokenResult = jwtService.GenerateRefreshToken();

        user.UpdateRefreshToken(refreshTokenResult.TokenHash, refreshTokenResult.ExpiresAt);
            
        await unitOfWork.CommitAsync(cancellationToken);

        var response = new LoginResponse(
            accessToken,
            refreshTokenResult.Token,
            refreshTokenResult.ExpiresAt
        );

        return Result<LoginResponse>.Success(response);
    }
}
