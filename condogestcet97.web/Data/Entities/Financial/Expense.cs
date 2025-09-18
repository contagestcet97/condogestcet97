using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace condogestcet97.web.Data.Entities.Financial
{
    public class Expense : IEntity
    {
        public int Id { get ; set ; }

        public decimal Amount { get; set ; }

        public Quota? Quota { get; set ; }

        public int QuotaId { get; set ; }

        [Column(TypeName = "varchar(250)")]
        [MaxLength(250)]
        public string Description { get; set ; }

        public bool IsFullyPaid { get; set ; }

        public int? ServiceId { get; set; }        
        public Service? Service { get; set; }       
    }
}
