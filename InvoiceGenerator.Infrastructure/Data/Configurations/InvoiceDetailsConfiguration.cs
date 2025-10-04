using InvoiceGenerator.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceGenerator.Infrastructure.Data.Configurations
{
    public class InvoiceDetailsConfiguration : IEntityTypeConfiguration<InvoiceDetails>
    {
        public void Configure(EntityTypeBuilder<InvoiceDetails> builder)
        {
            builder.ToTable("InvoiceDetails");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.SubTotal)
                    .IsRequired()
                    .HasPrecision(12, 2);
            
            builder.Property(x => x.Tax)
                    .IsRequired()
                    .HasPrecision(12, 2);
            
            builder.Property(x => x.Total)
                    .IsRequired()
                    .HasPrecision(12, 2);

            builder.HasOne(x => x.BillingAddress)
                   .WithMany(x => x.InvoiceDetails)
                   .HasForeignKey(x => x.BillingAddressId);
            
            builder.HasMany(x => x.InvoiceLineItems)
                   .WithOne(x => x.InvoiceDetails)
                   .HasForeignKey(x => x.InvoiceDetailsId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}