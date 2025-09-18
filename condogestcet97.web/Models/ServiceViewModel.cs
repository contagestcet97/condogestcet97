using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace condogestcet97.web.Models
{
    public class ServiceViewModel
    {
        public int Id { get; set; }

        public int? CondoId { get; set; }
        public string Description { get; set; }

        public string CompanyName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? FinishDate { get; set; }
        public bool IsRecurring { get; set; }
        public decimal? DefaultFee { get; set; }
    }
}
