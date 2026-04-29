using FinanceControl.Application.Abstractions;

namespace FinanceControl.Application.UseCases.Auth.Commands.Logout;

public record LogoutCommand : ICommand<LogoutResponse>;