using condogestcet97.web.Data.Entities.Financial;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace condogestcet97.web.Models
{
    public class QuotaViewModel
    {
        public int Id { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? PaidDate { get; set; }

        public bool IsPaid { get; set; }

        public bool InvoicesSent { get; set; }

        public decimal? LateFee { get; set; }

        public int CondoId { get; set; }

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

        public IEnumerable<SelectListItem>? Condos { get; set; }

        public IEnumerable<Expense>? Expenses { get; set; }

    }
}
