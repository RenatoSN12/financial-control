using FluentValidation;

namespace FinanceControl.Application.UseCases.Categories.Commands.CreateCategory;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty()
            .MaximumLength(255);
    }
}