using condogestcet97.web.Data.Entities.Financial;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace condogestcet97.web.Models
{
    public class ExpenseViewModel
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public int QuotaId { get; set; }

        public string Description { get; set; }

        public bool IsFullyPaid { get; set; }

        public int? ServiceId { get; set; }
    }
}
