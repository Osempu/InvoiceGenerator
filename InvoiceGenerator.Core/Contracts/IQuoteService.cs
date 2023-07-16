using InvoiceGenerator.Core.Models;

namespace InvoiceGenerator.Core.Contracts
{
    public interface IQuoteService
    {
        Invoice GenerateQuote(Invoice invoice);
        InvoiceDetails CalculateServicesCost(InvoiceDetails invoiceDetails);
    }
}