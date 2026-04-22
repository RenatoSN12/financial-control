using FinanceControl.Application.Abstractions;
using FinanceControl.Application.Common;
using FinanceControl.Domain.Entities;
using FinanceControl.Domain.Exceptions;
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
        try
        {
            var validationResult = await ValidateCommandAsync(
                command,
                cancellationToken
            );

            if (validationResult.IsFailure)
                return Result<CreateUserResponse>.Failure(validationResult.Error!);

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
        catch (DomainException ex)
        {
            var error = Error.Validation(ex.Message);
            return Result<CreateUserResponse>.Failure(error);
        }
        catch (Exception ex)
        {
            var error = Error.Unexpected(ex.Message);
            return Result<CreateUserResponse>.Failure(error);
        }
    }

    private async Task<Result> ValidateCommandAsync(
        CreateUserCommand command,
        CancellationToken cancellationToken
    )
    {
        var validator = new CreateUserCommandValidator();
        
        var validationResult = await validator.ValidateAsync(
            command,
            cancellationToken
        );
     
        if (!validationResult.IsValid)
        {
            var error = Error.Validation(validationResult.Errors.First().ErrorMessage);
            return Result.Failure(error);
        }

        var normalizedEmail = command.Email.Trim().ToLowerInvariant();
        var userExists = await userRepository.ExistsByEmailAsync(
            normalizedEmail,
            cancellationToken
        );

        if (userExists)
        {
            var error = Error.Validation("Já existe um usuário cadastrado com este e-mail");
            return Result.Failure(error);
        }

        return Result.Success();
    }
}