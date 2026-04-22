namespace FinanceControl.Application.UseCases.Abstractions;

public interface ICurrentUser
{
    Guid UserId { get; }
}