using FinanceControl.Domain.ValueObjects;

namespace FinanceControl.Domain.Entities;

public class InvoiceItem : Entity
{
    #region Properties

    public Guid InvoiceId { get; private set; }
    public Invoice Invoice { get; private set; } = null!;
    
    public Guid CardTransactionId { get; private set; }
    public CardTransaction CardTransaction { get; private set; } = null!;

    public int InstallmentNumber { get; private set; }

    #endregion

    #region Constructors

    private InvoiceItem(){}

    public InvoiceItem(
        Guid invoiceId,
        Guid cardTransactionId,
        int installmentNumber
    )
    {
        InvoiceId = invoiceId;
        CardTransactionId = cardTransactionId;
        InstallmentNumber = installmentNumber;
    }

    #endregion

    #region Functions



    #endregion
}