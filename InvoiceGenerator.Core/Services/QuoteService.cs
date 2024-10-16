using InvoiceGenerator.Core.Contracts;
using InvoiceGenerator.Core.Models;

namespace InvoiceGenerator.Core.Services
{
    public class QuoteService : IQuoteService
    {
        public QuoteService()
        {
               
        }

        public InvoiceDetails CalculateServicesCost(InvoiceDetails invoiceDetails)
        {
            foreach (var service in invoiceDetails.InvoiceLineItems!)
            {
                // service.Total = service.Quantity * service.UnitPrice;
                // invoiceDetails.SubTotal += service.Total;
            }

            invoiceDetails.Total = invoiceDetails.SubTotal + invoiceDetails.Tax;

            return invoiceDetails;
        }

        public Invoice GenerateQuote(Invoice invoice)
        {
            return invoice;
        }
    }
}
