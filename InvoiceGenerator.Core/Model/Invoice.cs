namespace InvoiceGenerator.Core.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public string Identifier { get; set; } = "";
        public DateOnly CreatedAt { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly ValidStartDate { get; set; }
        public DateOnly DueDate { get; set; }

        //Invoice Customer
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        //Invoice Details
        public int InvoiceDetailsId { get; set; }
        public InvoiceDetails? InvoiceDetails { get; set; }
    }
}