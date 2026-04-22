using FinanceControl.Domain.Entities;
using FinanceControl.Domain.ValueObjects;
using FinanceControl.Infrastructure.Mappings.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceControl.Infrastructure.Mappings;

public class CardMapping : BaseMapping<Card>
{
    public override void Configure(EntityTypeBuilder<Card> builder)
    {
        base.Configure(builder);

        builder.ToTable("card");

        builder.Property(c => c.Name)
            .IsRequired()
            .HasColumnName("name")
            .HasMaxLength(100);

        builder.OwnsOne(c => c.Limit)
            .Property(l => l.Amount)
            .HasColumnName("limit")
            .HasColumnType(MappingsConstants.MoneyColumnType)
            .IsRequired();
            
        builder.Property(c => c.ClosingDay)
            .IsRequired()
            .HasColumnName("closing_day")
            .HasColumnType("smallint");

        builder.Property(c => c.DueDay)
            .IsRequired()
            .HasColumnName("due_day")
            .HasColumnType("smallint");

        builder.Property(c => c.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        builder.HasOne(c => c.User)
            .WithMany(u => u.Cards)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
