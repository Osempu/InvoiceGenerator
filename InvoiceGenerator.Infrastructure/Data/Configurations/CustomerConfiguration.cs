using InvoiceGenerator.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceGenerator.Infrastructure.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(250);
            
            builder.Property(x => x.Description)
                    .HasMaxLength(500);
            
            builder.HasMany(x => x.Addresses)
                    .WithOne(x => x.Customer)
                    .HasForeignKey(x => x.CustomerId);
            
            builder.HasMany(x => x.Invoices)
                    .WithOne(x => x.Customer)
                    .HasForeignKey(x => x.CustomerId);
        }
    }
}