using System;
using System.Collections.Generic;
using System.Text;
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
            .WithOne(u => u.ownerProfile)
            .HasForeignKey<OwnerProfile>(op => op.UserId);
    }
}
