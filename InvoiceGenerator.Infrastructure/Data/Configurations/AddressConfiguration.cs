using System;
using InvoiceGenerator.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceGenerator.Infrastructure.Data.Configurations;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("Addresses");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Street)
                .IsRequired()
                .HasMaxLength(300);
        
        builder.Property(x => x.Street2)
                .HasMaxLength(300);

        builder.Property(x => x.PostalCode)
                .IsRequired()
                .HasMaxLength(30);

        builder.Property(x => x.City)
                .IsRequired()
                .HasMaxLength(100);

        builder.Property(x => x.State)
                .IsRequired()
                .HasMaxLength(100);

        builder.Property(x => x.Country)
                .IsRequired()
                .HasMaxLength(100);
        
        builder.Property(x => x.AddressType)
                .IsRequired()
                .HasMaxLength(30);

        builder.HasOne(x => x.Customer)
                .WithMany(x => x.Addresses)
                .HasForeignKey(x => x.CustomerId);
    }
}
