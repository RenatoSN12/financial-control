using FinanceControl.Domain.Entities;
using FinanceControl.Domain.ValueObjects;
using FinanceControl.Infrastructure.Mappings.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceControl.Infrastructure.Mappings;

public class CardTransactionMapping : BaseMapping<CardTransaction>
{
    public override void Configure(EntityTypeBuilder<CardTransaction> builder)
    {
        base.Configure(builder);

        builder.ToTable("card_transaction");

        builder.Property(ct => ct.CardId)
            .HasColumnName("card_id")
            .IsRequired();

        builder.HasOne(ct => ct.Card)
            .WithMany(c => c.CardTransactions)
            .HasForeignKey(ct => ct.CardId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.OwnsOne(ct => ct.Amount)
            .Property(a => a.Amount)
            .HasColumnName("amount")
            .HasColumnType(MappingsConstants.MoneyColumnType)
            .IsRequired();

        builder.Property(ct => ct.Description)
            .IsRequired()
            .HasColumnName("description")
            .HasMaxLength(255);

        builder.Property(ct => ct.Installments)
            .HasColumnName("installments")
            .HasColumnType("smallint")
            .IsRequired();

        builder.Property(ct => ct.PurchaseDate)
            .HasColumnName("purchase_date")
            .HasColumnType("date")
            .IsRequired();

        builder.Property(ct => ct.IsRecurring)
            .HasColumnName("is_recurring")
            .HasColumnType("boolean")
            .IsRequired();
    }
}
