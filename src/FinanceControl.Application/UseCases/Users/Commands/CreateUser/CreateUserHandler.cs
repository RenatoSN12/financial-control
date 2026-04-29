using FinanceControl.Application.Abstractions;
using FinanceControl.Application.Common;
using FinanceControl.Domain.Entities;
using FinanceControl.Domain.Repositories;
using FinanceControl.Domain.Services;
using FinanceControl.Domain.ValueObjects;

namespace FinanceControl.Application.UseCases.Users.Commands.CreateUser;

public class CreateUserHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IPasswordHasher passwordHasher
) : ICommandHandler<CreateUserCommand, CreateUserResponse>
{
    public async Task<Result<CreateUserResponse>> HandleAsync(
        CreateUserCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var normalizedEmail = command.Email.Trim().ToLowerInvariant();
        var emailAlreadyInUse = await userRepository.ExistsByEmailAsync(
            normalizedEmail,
            cancellationToken
        );

        if (emailAlreadyInUse)
            return Result<CreateUserResponse>.Failure(
                Error.Validation("Já existe um usuário cadastrado com este e-mail")
            );

        var password = Password.Create(command.Password);
        var passwordHash = passwordHasher.Hash(password.Value);

        var user = User.Create(
            command.FirstName,
            command.LastName,
            command.Email,
            passwordHash
        );

        await userRepository.AddAsync(
            user,
            cancellationToken
        );

        await unitOfWork.CommitAsync(cancellationToken);

        var response = new CreateUserResponse(
            user.Id,
            user.Name.FullName
        );

        return Result<CreateUserResponse>.Success(response);
    }
}
