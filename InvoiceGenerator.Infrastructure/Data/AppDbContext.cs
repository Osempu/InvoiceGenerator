using Microsoft.EntityFrameworkCore;
using InvoiceGenerator.Core.Models;
using InvoiceGenerator.Core.Model;
using InvoiceGenerator.Infrastructure.Data.Configurations;
using InvoiceGenerator.Infrastructure.Data.Seeding;

namespace InvoiceGenerator.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql("Server=localhost;Database=InvoiceDb;Port=5432;User Id =postgres;Password=$Oscared94;")
                    .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name })
                    .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CustomerConfiguration());
            builder.ApplyConfiguration(new AddressConfiguration());
            builder.ApplyConfiguration(new ItemConfiguration());
            builder.ApplyConfiguration(new InvoiceConfiguration());
            builder.ApplyConfiguration(new InvoiceDetailsConfiguration());
            builder.ApplyConfiguration(new InvoiceLineItemConfiguration());

            builder.CustomerSeed();
            builder.ItemSeed();
            builder.InvoiceSeed();
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceDetails> InvoiceDetails { get; set; }
        public DbSet<InvoiceLineItem> InvoiceLineItems { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}