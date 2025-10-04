using InvoiceGenerator.Core.Model;

namespace InvoiceGenerator.Core.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Details { get; set; }
        public decimal Price { get; set; }

        public ICollection<InvoiceLineItem>? InvoiceLineItems { get; set; }
    }
}