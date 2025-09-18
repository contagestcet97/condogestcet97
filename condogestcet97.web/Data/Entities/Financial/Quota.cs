using condogestcet97.web.Data.Entities.Condominium;
using System.ComponentModel.DataAnnotations.Schema;

namespace condogestcet97.web.Data.Entities.Financial
{
    public class Quota : IEntity
    {
        public int Id { get ; set; }

        public DateTime DueDate { get; set; }

        public DateTime? PaidDate { get; set; }

        public bool IsPaid { get; set; }

        public bool InvoicesSent { get; set; } 

        public decimal? LateFee { get; set; }

        public int CondoId { get; set; }

        public ICollection<Expense> Expenses { get; set; } = new List<Expense>();

        [NotMapped]
        public decimal PaymentValue
        {
            get
            {
                if (Expenses == null || ApartmentsCount == 0)
                    return 0;

                return Expenses.Sum(e => e.Amount) / ApartmentsCount;
            }
        }
        public int ApartmentsCount { get; set; }

    }
}
