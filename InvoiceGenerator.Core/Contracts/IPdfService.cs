using InvoiceGenerator.Core.Models;

namespace InvoiceGenerator.Core.Contracts
{
    public interface IPdfService
    {
        void GeneratePdf(Invoice invoice);
        
    }
}