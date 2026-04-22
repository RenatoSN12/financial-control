namespace FinanceControl.Domain.Exceptions;

public sealed class InvalidPasswordException(string message) : DomainException(message);