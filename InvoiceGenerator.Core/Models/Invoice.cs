namespace InvoiceGenerator.Core.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public DateOnly ValidUntil { get; set; }

        //Invoice Client
        public int ClientId { get; set; }
        public Client? Client { get; set; }

        //Invoice Details
        public int InvoiceDetailsId { get; set; }
        public InvoiceDetails? InvoiceDetails { get; set; }
    }
}