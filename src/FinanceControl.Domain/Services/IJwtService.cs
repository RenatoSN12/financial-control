namespace FinanceControl.Domain.Services;

public interface IJwtService
{
    string GenerateAccessToken(Guid userId, string email);
    RefreshTokenResult GenerateRefreshToken();
    string HashRefreshToken(string refreshToken);
}