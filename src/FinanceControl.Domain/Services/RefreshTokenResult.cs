namespace FinanceControl.Domain.Services;

public sealed record RefreshTokenResult(string Token, string TokenHash, DateTime ExpiresAt);
