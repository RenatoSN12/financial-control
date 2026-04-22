namespace FinanceControl.Domain.Exceptions;

public abstract class BusinessRuleException(string message) : DomainException(message);