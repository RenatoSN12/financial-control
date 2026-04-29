using FinanceControl.Application.Abstractions;
using FinanceControl.Application.Common;
using FinanceControl.Domain.Entities;
using FinanceControl.Domain.Repositories;
using FinanceControl.Domain.Services;

namespace FinanceControl.Application.UseCases.Auth.Commands.RefreshToken;

public class RefreshTokenHandler(
    IUserRepository userRepository,
    IJwtService jwtService,
    IUnitOfWork unitOfWork
) : ICommandHandler<RefreshTokenCommand, RefreshTokenResponse>
{
    public async Task<Result<RefreshTokenResponse>> HandleAsync(
        RefreshTokenCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var refreshTokenHash = jwtService.HashRefreshToken(command.RefreshToken);

        var user = await userRepository.GetByRefreshTokenHashAsync(
            refreshTokenHash,
            cancellationToken
        );

        var validationResult = ValidateUser(user);
        if (validationResult.IsFailure)
            return Result<RefreshTokenResponse>.Failure(validationResult.Error!);
        
        var validatedUser = validationResult.Value!;

        var accessToken = jwtService.GenerateAccessToken(
            validatedUser.Id,
            validatedUser.Email.Address
        );

        var newRefreshToken = jwtService.GenerateRefreshToken();

        validatedUser.UpdateRefreshToken(newRefreshToken.TokenHash, newRefreshToken.ExpiresAt);

        await unitOfWork.CommitAsync(cancellationToken);

        var response = new RefreshTokenResponse(
            accessToken,
            newRefreshToken.Token
        );

        return Result<RefreshTokenResponse>.Success(response);
    }

    private static Result<User> ValidateUser(User? user)
    {
        if (user == null)
            return Result<User>.Failure(Error.NotFound("Usuário não encontrado."));

        if (!user.IsActive)
            return Result<User>.Failure(Error.Unauthorized("Usuário inativo."));

        if (user.IsRefreshTokenExpired())
            return Result<User>.Failure(Error.Unauthorized("Refresh token expirado."));

        return Result<User>.Success(user);
    }
}
