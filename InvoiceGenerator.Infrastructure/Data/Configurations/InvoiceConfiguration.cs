using InvoiceGenerator.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceGenerator.Infrastructure.Data.Configurations
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property( x => x.Date)
                    .IsRequired()
                    .HasDefaultValue(DateOnly.FromDateTime(DateTime.Now));

            builder.Property( x => x.ValidUntil)
                    .IsRequired()
                    .HasDefaultValue(DateOnly.FromDateTime(DateTime.Now.AddDays(9)));
            
            builder.HasOne(x => x.Client)
                    .WithMany(x => x.Invoices)
                    .HasForeignKey(x => x.ClientId);

            builder.HasOne(x => x.InvoiceDetails)
                    .WithOne( x=> x.Invoice);
        }
    }
}