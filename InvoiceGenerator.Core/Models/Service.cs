namespace InvoiceGenerator.Core.Models
{
    public class Service
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string? ServiceDetail { get; set; }
        public Decimal UnitPrice { get; set; }
        public Decimal Total { get; set; }

        public ICollection<InvoiceDetails> InvoiceDetails { get; set; }
    }
}