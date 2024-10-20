using InvoiceGenerator.Application.Contracts;
using InvoiceGenerator.Core.Models;
using InvoiceGenerator.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InvoiceGenerator.Infrastructure.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly AppDbContext context;
        public InvoiceRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Invoice?> CreateInvoiceAsync(Invoice invoice)
        {
            var newInvoice = context.Add(invoice).Entity;
            await context.SaveChangesAsync();
            return newInvoice;
        }

        public async Task DeleteInvoiceAsync(int id)
        {
            var invoice = await context.Invoices.FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new NullReferenceException($"Invoice with id {id} does not exist in database");

            context.Remove(invoice);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Invoice>> GetAllInvoicesAsync()
        {
            var invoices = await context.Invoices.AsNoTracking()
                                                .ToListAsync();
            return invoices;
        }

        public async Task<Invoice?> GetInvoiceAsync(int id)
        {
            return await context.Invoices.AsNoTracking()
                                         .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Invoice?> UpdateInvoiceAsync(Invoice invoice)
        {
            context.Entry(invoice).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return invoice;
        }
    }
}