using FinanceControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceControl.Infrastructure.Mappings;

public class UserMapping : BaseMapping<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.ToTable("user");

        builder.OwnsOne(u => u.Name, name =>
        {
            name.Property(n => n.FirstName)
                .IsRequired()
                .HasColumnName("first_name")
                .HasMaxLength(100);
        });

        builder.OwnsOne(u => u.Name, name =>
        {
            name.Property(n => n.LastName)
                .IsRequired()
                .HasColumnName("last_name")
                .HasMaxLength(150);
        });

        builder.OwnsOne(u => u.Email, email =>
        {
            email.Property(e => e.Address)
                .IsRequired()
                .HasColumnName("email")
                .HasMaxLength(255);
        });

        builder.Property(u => u.PasswordHash)
            .IsRequired()
            .HasColumnName("password_hash");

        builder.Property(u => u.IsActive)
            .IsRequired()
            .HasColumnName("is_active");

        builder.Property(u => u.RefreshToken)
            .HasColumnName("refresh_token");

        builder.Property(u => u.RefreshTokenExpiresAt)
            .HasColumnName("refresh_token_expires_at")
            .HasColumnType("timestamp with time zone");
    }
}
