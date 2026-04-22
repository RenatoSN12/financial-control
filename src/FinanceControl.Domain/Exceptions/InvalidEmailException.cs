namespace FinanceControl.Domain.Exceptions;

public sealed class InvalidEmailException(string message) : DomainException(message);