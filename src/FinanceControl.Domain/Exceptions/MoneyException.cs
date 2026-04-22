namespace FinanceControl.Domain.Exceptions;

public sealed class MoneyException(string message) : BusinessRuleException(message);