using InvoiceGenerator.Core.Models;

namespace InvoiceGenerator.Core.Contracts;

public interface IInvoiceService
{
    public IEnumerable<Invoice> GetAllInvoices();
    public Invoice CreateInvoice(Invoice invoice);
    public Invoice UpdateInvoice(Invoice invoice);
    public void DeleteInvoice(int id);
}
