namespace FinanceControl.Application.UseCases.Auth.Commands.Login;

public record LoginResponse(
    string AccessToken,
    string RefreshToken,
    DateTime RefreshTokenExpiresAt
);
