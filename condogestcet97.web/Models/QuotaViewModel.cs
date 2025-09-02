namespace condogestcet97.web.Models
{
    public class QuotaViewModel
    {
        public int Id { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? PaidDate { get; set; }

        public bool IsPaid { get; set; }

        public decimal? LateFee { get; set; }

        public decimal PaymentValue { get; set; }

        public int? CondoId { get; set; }

    }
}
