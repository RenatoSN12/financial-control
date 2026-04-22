using FinanceControl.Domain.ValueObjects;

namespace FinanceControl.Domain.Entities;

public class Card : AggregateRoot
{
    #region Properties
    
    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;
    
    public string Name { get; private set; } = string.Empty;
    public Money Limit { get; private set; } = null!;
    public int ClosingDay { get; private set; }
    public int DueDay { get; private set; }
    private readonly List<CardTransaction> _cardTransactions = [];
    public IReadOnlyCollection<CardTransaction> CardTransactions => _cardTransactions;
    private readonly List<Invoice> _invoices = [];
    public IReadOnlyCollection<Invoice> Invoices => _invoices;

    #endregion

    #region Constructors

    private Card(){}

    public Card(
        Guid userId,
        string name,
        Money limit,
        int closingDay,
        int dueDay
    )
    {
        UserId = userId;
        Name = name;
        Limit = limit;
        ClosingDay = closingDay;
        DueDay = dueDay;
    }

    #endregion
}