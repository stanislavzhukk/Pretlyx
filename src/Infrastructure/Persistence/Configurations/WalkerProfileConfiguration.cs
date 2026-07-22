using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public sealed class WalkerProfileConfiguration : IEntityTypeConfiguration<WalkerProfile>
{
    public void Configure(EntityTypeBuilder<WalkerProfile> builder)
    {
        builder.HasKey(op => op.Id);
            
        builder.Property(op => op.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .HasOne(op => op.User)
            .WithOne(u => u.WalkerProfile)
            .HasForeignKey<WalkerProfile>(op => op.UserId);
    }
}
