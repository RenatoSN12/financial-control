using FinanceControl.Application.Abstractions;
using FinanceControl.Application.Common;
using FinanceControl.Domain.Exceptions;
using FinanceControl.Domain.Repositories;
using FinanceControl.Domain.Services;
using FluentValidation;

namespace FinanceControl.Application.UseCases.Auth.Commands.Login;

public class LoginHandler(
    IValidator<LoginCommand> validator,
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
        try
        {
            var validationResult = await ValidateCommandAsync(command, cancellationToken);
            if (validationResult.IsFailure)
                return Result<LoginResponse>.Failure(validationResult.Error!);

            var normalizedEmail = command.Email.Trim().ToLowerInvariant();
            
            var user = await userRepository.GetByEmailAsync(normalizedEmail, cancellationToken);
            if (user == null)
                return Result<LoginResponse>.Failure(
                    Error.Validation("Email ou senha inválidos.")
                );

            if (!user.IsActive)
                return Result<LoginResponse>.Failure(
                    Error.Validation("O usuário está inativo.")
                );

            if (!passwordHasher.Verify(command.Password, user.PasswordHash))
                return Result<LoginResponse>.Failure(
                    Error.Validation("Email ou senha inválidos.")
                );

            var accessToken = jwtService.GenerateAccessToken(user.Id, user.Email.Address);
            var refreshTokenResult = jwtService.GenerateRefreshToken();

            user.UpdateRefreshToken(refreshTokenResult.TokenHash, refreshTokenResult.ExpiresAt);
            
            await unitOfWork.CommitAsync(cancellationToken);

            return Result<LoginResponse>.Success(
                new LoginResponse(
                    accessToken,
                    refreshTokenResult.Token,
                    refreshTokenResult.ExpiresAt
                )
            );
        }
        catch (DomainException ex)
        {
            return Result<LoginResponse>.Failure(Error.Validation(ex.Message));
        }
        catch (Exception ex)
        {
            return Result<LoginResponse>.Failure(
                Error.Unexpected($"Erro inesperado: {ex.Message}")
            );
        }
    }

    private async Task<Result> ValidateCommandAsync(
        LoginCommand command,
        CancellationToken cancellationToken
    )
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
            return Result.Failure(Error.Validation(errors));
        }

        return Result.Success();
    }
}
