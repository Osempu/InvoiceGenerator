using InvoiceGenerator.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceGenerator.Infrastructure.Data.Configurations
{
    public class InvoiceDetailsConfiguration : IEntityTypeConfiguration<InvoiceDetails>
    {
        public void Configure(EntityTypeBuilder<InvoiceDetails> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Equipment)
                    .IsRequired()
                    .HasMaxLength(250);
            
            builder.HasMany(x => x.Services)
                    .WithMany(x => x.InvoiceDetails)
                    .UsingEntity(j => j.ToTable("InvDetailsServices"));
            
            builder.Property(x => x.SubTotal)
                    .HasPrecision(12, 2);
            
            builder.Property(x => x.IVA).IsRequired().HasMaxLength(100);

            builder.Property(x => x.Total)
                    .HasPrecision(12, 2);
        }
    }
}