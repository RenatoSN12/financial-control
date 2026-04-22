using FinanceControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceControl.Infrastructure.Mappings;

public class InvoiceMapping : BaseMapping<Invoice>
{
    public override void Configure(EntityTypeBuilder<Invoice> builder)
    {
        base.Configure(builder);

        builder.ToTable("invoice");

        builder.Property(i => i.CardId)
            .HasColumnName("card_id")
            .IsRequired();

        builder.HasOne(i => i.Card)
            .WithMany(c => c.Invoices)
            .HasForeignKey(i => i.CardId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(i => i.Month)
            .HasColumnName("month")
            .HasColumnType("smallint")
            .IsRequired();

        builder.Property(i => i.Year)
            .HasColumnName("year")
            .HasColumnType("smallint")
            .IsRequired();

        builder.Property(i => i.ClosingDate)
            .HasColumnName("closing_date")
            .HasColumnType("date")
            .IsRequired();

        builder.Property(i => i.DueDate)
            .HasColumnName("due_date")
            .HasColumnType("date")
            .IsRequired();
    }
}
