using FinanceControl.Domain.Entities;
using FinanceControl.Infrastructure.Mappings.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceControl.Infrastructure.Mappings;

public class TransactionMapping : BaseMapping<Transaction>
{
    public override void Configure(EntityTypeBuilder<Transaction> builder)
    {
        base.Configure(builder);

        builder.ToTable("transaction");

        builder.Property(t => t.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        builder.HasOne(t => t.User)
            .WithMany(u => u.Transactions)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Property(t => t.CategoryId)
            .HasColumnName("category_id")
            .IsRequired();

        builder.HasOne(t => t.Category)
            .WithMany(c => c.Transactions)
            .HasForeignKey(t => t.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.OwnsOne(t => t.Amount)
            .Property(a => a.Amount)
            .HasColumnName("amount")
            .HasColumnType(MappingsConstants.MoneyColumnType)
            .IsRequired();

        builder.Property(t => t.Type)
            .HasColumnName("type")
            .HasColumnType("smallint")
            .IsRequired();

        builder.Property(t => t.Date)
            .HasColumnName("date")
            .HasColumnType("date")
            .IsRequired();

        builder.Property(t => t.Description)
            .IsRequired()
            .HasColumnName("description")
            .HasColumnType("text");
    }
}