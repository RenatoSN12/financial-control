using FinanceControl.Application.Abstractions;

namespace FinanceControl.Application.UseCases.Users.Commands.CreateUser;

public record CreateUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password
) : ICommand<CreateUserResponse>;
