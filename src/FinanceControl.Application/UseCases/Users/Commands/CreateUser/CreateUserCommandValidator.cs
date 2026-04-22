using FluentValidation;

namespace FinanceControl.Application.UseCases.Users.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
                .WithMessage("O primeiro nome é obrigatório")
            .MaximumLength(100)
                .WithMessage("O primeiro nome não pode ter mais de 100 caracteres");

        RuleFor(x => x.LastName)
            .NotEmpty()
                .WithMessage("O sobrenome é obrigatório")
            .MaximumLength(150)
                .WithMessage("O sobrenome não pode ter mais de 150 caracteres");

        RuleFor(x => x.Email)
            .NotEmpty()
                .WithMessage("O e-mail é obrigatório")
            .MaximumLength(255)
                .WithMessage("O e-mail não pode ter mais de 255 caracteres")
            .EmailAddress()
                .WithMessage("O e-mail não está em um formato válido");

        RuleFor(x => x.Password)
            .NotEmpty()
                .WithMessage("A senha é obrigatória")
            .MinimumLength(8)
                .WithMessage("A senha deve conter no mínimo 8 caracteres")
            .MaximumLength(100)
                .WithMessage("A senha deve conter no máximo 100 caracteres")
            .Matches("[A-Z]")
                .WithMessage("A senha deve conter pelo menos uma letra maiúscula")
            .Matches("[a-z]")
                .WithMessage("A senha deve conter pelo menos uma letra minúscula")
            .Matches("[0-9]")
                .WithMessage("A senha deve conter pelo menos um número")
            .Matches("[^a-zA-Z0-9]")
                .WithMessage("A senha deve conter pelo menos um caractere especial");
    }
}