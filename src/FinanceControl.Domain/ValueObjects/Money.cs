using FinanceControl.Domain.Exceptions;

namespace FinanceControl.Domain.ValueObjects;

public class Money : ValueObject
{
    public decimal Amount { get; private set; }

    private Money(){}

    public Money(decimal amount)
    {
        SetAmount(amount);
    }

    private void SetAmount(decimal amount)
    {
        if (amount < 0)
            throw new MoneyException("O valor não pode ser menor que 0");

        Amount = amount;
    }
    public static Money operator +(Money a, Money b)
        => new(a.Amount + b.Amount);

    public static Money operator -(Money a, Money b)
        => new(a.Amount - b.Amount);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
    }
}