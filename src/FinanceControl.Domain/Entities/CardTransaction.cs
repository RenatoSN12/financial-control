using FinanceControl.Domain.ValueObjects;

namespace FinanceControl.Domain.Entities;

public class CardTransaction : AggregateRoot
{
    #region Properties

    public Guid CardId { get; private set; }
    public Card Card { get; private set; } = null!;
    
    public Money Amount { get; private set; } = null!;
    public string Description { get; private set; } = string.Empty;
    public int Installments { get; private set; }
    public DateOnly PurchaseDate { get; private set; }
    public bool IsRecurring { get; private set; }

    private readonly List<InvoiceItem> _invoiceItems = [];
    public IReadOnlyCollection<InvoiceItem> InvoiceItems => _invoiceItems;

    #endregion

    #region Constructors

    private CardTransaction(){}

    public CardTransaction(
        Guid cardId,
        Money amount,
        string description,
        bool isRecurring,
        int installments,
        DateOnly purchaseDate
    )
    {
        CardId = cardId;
        Amount = amount;
        Description = description;
        IsRecurring = isRecurring;
        Installments = installments;
        PurchaseDate = purchaseDate;
    }

    #endregion
}