namespace FinanceControl.Domain.Exceptions;

public sealed class InvalidTextException(string message) : DomainException(message);