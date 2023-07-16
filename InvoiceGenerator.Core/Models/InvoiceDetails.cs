namespace InvoiceGenerator.Core.Models
{
    public class InvoiceDetails
    {
        public int Id { get; set; }
        public string? Equipment { get; set; }
        public ICollection<Service>? Services { get; set; }

        public Decimal SubTotal { get; set; }
        public int IVA { get; private set;} = 18;
        public Decimal Total { get; set; }

        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
    }
}