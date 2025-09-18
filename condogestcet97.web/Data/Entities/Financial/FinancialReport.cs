namespace condogestcet97.web.Data.Entities.Financial
{
    public class FinancialReport : IEntity
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsPdfSent { get; set; }

        public decimal ExpensesTotal { get; set; }

        public decimal RevenueTotal { get; set; }

        public decimal NetTotal => RevenueTotal - ExpensesTotal;
    }
}
