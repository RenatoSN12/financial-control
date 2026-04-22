using FinanceControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceControl.Infrastructure.Mappings;

public class CategoryMapping : BaseMapping<Category>
{
    public override void Configure(EntityTypeBuilder<Category> builder)
    {
        base.Configure(builder);

        builder.ToTable("category");

        builder.Property(c => c.Name)
            .IsRequired()
            .HasColumnName("name")
            .HasMaxLength(100);

        builder.Property(c => c.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        builder.HasOne(c => c.User)
            .WithMany(u => u.Categories)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}