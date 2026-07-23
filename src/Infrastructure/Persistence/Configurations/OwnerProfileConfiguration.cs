using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class OwnerProfileConfiguration : IEntityTypeConfiguration<OwnerProfile>
{
    public void Configure(EntityTypeBuilder<OwnerProfile> builder)
    {
        builder.HasKey(op => op.Id);

        builder.Property(op => op.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .HasOne(op => op.User)
            .WithOne(u => u.OwnerProfile)
            .HasForeignKey<OwnerProfile>(op => op.UserId);

        builder.Property(op => op.PhoneNumber)
            .HasMaxLength(16)
            .IsUnicode(false);
    }
}
