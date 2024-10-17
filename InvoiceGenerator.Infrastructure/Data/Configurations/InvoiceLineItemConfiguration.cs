using System;
using InvoiceGenerator.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceGenerator.Infrastructure.Data.Configurations;

public class InvoiceLineItemConfiguration : IEntityTypeConfiguration<InvoiceLineItem>
{
    public void Configure(EntityTypeBuilder<InvoiceLineItem> builder)
    {
        builder.ToTable("InvoiceLineItems");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Quantity)
                .IsRequired();
        
        builder.HasOne(x => x.Item)
                .WithMany(x => x.InvoiceLineItems)
                .HasForeignKey(x => x.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.InvoiceDetails)
                .WithMany(x => x.InvoiceLineItems)
                .HasForeignKey(x => x.InvoiceDetailsId)
                .OnDelete(DeleteBehavior.Cascade);
    }
}
