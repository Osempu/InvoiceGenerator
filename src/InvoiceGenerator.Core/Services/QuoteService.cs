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
            if (invoiceDetails.InvoiceLineItems != null && invoiceDetails.InvoiceLineItems.Count > 0)
            {
                foreach (var lineItem in invoiceDetails.InvoiceLineItems!)
                {
                    decimal price = lineItem.Item?.Price ?? 0m;
                    invoiceDetails.SubTotal += price * lineItem.Quantity;
                }

                invoiceDetails.Total = invoiceDetails.SubTotal + invoiceDetails.Tax;
                return invoiceDetails;
            }

            return invoiceDetails;
        }

        public Invoice GenerateQuote(Invoice invoice)
        {
            return invoice;
        }
    }
}
