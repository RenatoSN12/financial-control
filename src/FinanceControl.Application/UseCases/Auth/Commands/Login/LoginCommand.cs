using FinanceControl.Application.Abstractions;

namespace FinanceControl.Application.UseCases.Auth.Commands.Login;

public record LoginCommand(
    string Email,
    string Password
) : ICommand<LoginResponse>;
