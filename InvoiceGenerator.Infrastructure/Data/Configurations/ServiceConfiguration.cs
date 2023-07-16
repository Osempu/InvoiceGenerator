using InvoiceGenerator.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceGenerator.Infrastructure.Data.Configurations
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Quantity)
                    .IsRequired();
            
            builder.Property(x => x.ServiceDetail)
                    .IsRequired()
                    .HasMaxLength(300);
            
            builder.Property(x => x.UnitPrice)
                    .HasPrecision(12, 2);
            
            builder.Property(x => x.Total)
                    .HasPrecision(12, 2);
        }
    }
}