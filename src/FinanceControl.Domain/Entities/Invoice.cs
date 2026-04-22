namespace FinanceControl.Domain.Entities;

public class Invoice : AggregateRoot
{
    public Guid CardId { get; private set; }
    public Card Card { get; private set; } = null!;
    public int Month { get; private set; }
    public int Year { get; private set; }
    public DateOnly ClosingDate { get; private set; }
    public DateOnly DueDate { get; private set; }

    private readonly List<InvoiceItem> _items = [];
    public IReadOnlyCollection<InvoiceItem> Items => _items;

    #region Constructors

    private Invoice(){}

    public Invoice(
        Guid cardId,
        int month,
        int year,
        DateOnly closingDate,
        DateOnly dueDate
    )
    {
        CardId = cardId;
        Month = month;
        Year = year;
        ClosingDate = closingDate;
        DueDate = dueDate;
    }

    #endregion

    public void AddItem(InvoiceItem item)
        => _items.Add(item);
}