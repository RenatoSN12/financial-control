namespace FinanceControl.Domain.Exceptions;

public sealed class InvalidGuidException(string message) : DomainException(message);