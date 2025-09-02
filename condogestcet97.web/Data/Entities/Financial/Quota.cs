namespace condogestcet97.web.Data.Entities.Financial
{
    public class Quota : IEntity
    {
        public int Id { get ; set; }

        public DateTime DueDate { get; set; }

        public DateTime? PaidDate { get; set; }

        public bool IsPaid { get; set; }

        public decimal? LateFee { get; set; }

        public decimal PaymentValue { get; set; }

        public int? CondoId { get; set; }
    }
}
