using Microsoft.EntityFrameworkCore;
using InvoiceGenerator.Core.Models;
using InvoiceGenerator.Core.Model;

namespace InvoiceGenerator.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("DataSource = Demo.db");
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceDetails> InvoiceDetails { get; set; }
        public DbSet<InvoiceLineItem> InvoiceLineItems { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}