using InvoiceGenerator.Application.Contracts;
using InvoiceGenerator.Core.Contracts;
using InvoiceGenerator.Core.Models;
using InvoiceGenerator.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InvoiceGenerator.Infrastructure.Repositories
{
    public class InvoiceDetailsRepository : IInvoiceDetailsRepository
    {
        private readonly AppDbContext context;

        public InvoiceDetailsRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<InvoiceDetails> CreateInvoiceDetailsAsync(InvoiceDetails invoiceDetails)
        {
            var newInvoiceDetails = context.Add(invoiceDetails).Entity;
            await context.SaveChangesAsync();
            return newInvoiceDetails;
        }

        public async Task DeleteInvoiceDetailsAsync(int id)
        {
            var invoiceDetails = context.InvoiceDetails?.AsNoTracking()
                                                        .FirstOrDefaultAsync(x => x.Id == id);
            context.Remove(invoiceDetails);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<InvoiceDetails>> GetAllInvoiceDetailsAsync()
        {
            var invoiceDetails =  await context.InvoiceDetails.AsNoTracking()
                                                                .ToListAsync();
            return invoiceDetails;
        }

        public async Task<InvoiceDetails?> GetInvoiceDetailsAsync(int id)
        {
            var invoiceDetails = await context.InvoiceDetails.AsNoTracking()
                                                                .FirstOrDefaultAsync(x => x.Id == id);
            return invoiceDetails;
        }

        public async Task<InvoiceDetails> UpdateInvoiceDetailsAsync(InvoiceDetails invoiceDetails)
        {
            context.Entry(invoiceDetails).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return invoiceDetails;
        }
    }
}