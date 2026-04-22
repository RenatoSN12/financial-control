using FinanceControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceControl.Infrastructure.Mappings;

public class InvoiceItemMapping : BaseMapping<InvoiceItem>
{
    public override void Configure(EntityTypeBuilder<InvoiceItem> builder)
    {
        base.Configure(builder);

        builder.ToTable("invoice_item");

        builder.Property(ii => ii.InvoiceId)
            .HasColumnName("invoice_id")
            .IsRequired();

        builder.HasOne(ii => ii.Invoice)
            .WithMany(i => i.Items)
            .HasForeignKey(ii => ii.InvoiceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(ii => ii.CardTransactionId)
            .HasColumnName("card_transaction_id")
            .IsRequired();

        builder.HasOne(ii => ii.CardTransaction)
            .WithMany(ct => ct.InvoiceItems)
            .HasForeignKey(ii => ii.CardTransactionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(ii => ii.InstallmentNumber)
            .HasColumnName("installment_number")
            .HasColumnType("smallint")
            .IsRequired();
    }
}