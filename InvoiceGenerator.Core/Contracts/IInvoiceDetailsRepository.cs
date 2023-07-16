using InvoiceGenerator.Core.Models;

namespace InvoiceGenerator.Core.Contracts
{
    public interface IInvoiceDetailsRepository
    {
        Task<IEnumerable<InvoiceDetails>> GetAllInvoiceDetailsAsync();
        Task<InvoiceDetails> GetInvoiceDetailsAsync(int id);
        Task<InvoiceDetails> CreateInvoiceDetailsAsync(InvoiceDetails invoiceDetails);
        Task<InvoiceDetails> UpdateInvoiceDetailsAsync(InvoiceDetails invoiceDetails);
        Task DeleteInvoiceDetailsAsync(int id); 
    }
}