using InvoiceGenerator.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceGenerator.Infrastructure.Data.Configurations
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("Invoices");
            
            builder.HasKey(x => x.Id);

            builder.Property( x => x.Identifier)
                    .IsRequired()
                    .HasMaxLength(50);

            builder.Property( x => x.CreatedAt)
                    .IsRequired()
                    .HasDefaultValue(DateOnly.FromDateTime(DateTime.Now));

            builder.Property( x => x.ValidStartDate)
                    .IsRequired()
                    .HasDefaultValue(DateOnly.FromDateTime(DateTime.Now));

            builder.Property( x => x.DueDate)
                    .IsRequired();
            
            builder.HasOne(x => x.Customer)
                    .WithMany(x => x.Invoices)
                    .HasForeignKey(x => x.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.InvoiceDetails)
                    .WithOne( x=> x.Invoice)
                    .HasForeignKey<InvoiceDetails>(x => x.InvoiceId)
                    .OnDelete(DeleteBehavior.Cascade);
                    
        }
    }
}