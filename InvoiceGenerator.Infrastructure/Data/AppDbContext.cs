using Microsoft.EntityFrameworkCore;
using InvoiceGenerator.Core.Models;

namespace InvoiceGenerator.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("DataSource = Demo.db");
        }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<InvoiceDetails> InvoiceDetails { get; set; }
    }
}