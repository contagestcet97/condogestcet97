namespace condogestcet97.web.Data.Entities.Financial
{
    public class Payment
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaidDate { get; set; }

        public string? UserId { get; set; }

        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }

    }
}
