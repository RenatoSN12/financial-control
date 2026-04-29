using FinanceControl.Application.Abstractions;

namespace FinanceControl.Application.UseCases.Auth.Commands.RefreshToken;

public record RefreshTokenCommand(
    string RefreshToken
) : ICommand<RefreshTokenResponse>;