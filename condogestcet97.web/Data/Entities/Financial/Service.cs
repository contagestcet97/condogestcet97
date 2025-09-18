using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace condogestcet97.web.Data.Entities.Financial
{
    public class Service : IEntity
    {
        public int Id { get; set; }

        public int CondoId { get; set; }

        [Column(TypeName = "varchar(250)")]
        [MaxLength(250)]
        public string Description { get; set; }

        [Column(TypeName = "varchar(100)")]
        [MaxLength(100)]
        public string CompanyName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? FinishDate { get; set; }
        public bool IsRecurring { get; set; }
        public decimal? DefaultFee { get; set; } 
    }
}
