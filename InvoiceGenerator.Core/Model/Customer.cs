using InvoiceGenerator.Core.Model;

namespace InvoiceGenerator.Core.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        
        public ICollection<Address>? Addresses { get; set; }
        public ICollection<Invoice>? Invoices { get; set; }
    }
}