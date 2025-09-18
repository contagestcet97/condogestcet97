using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace condogestcet97.web.Data.Entities.Financial
{
    public class Payment : IEntity
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaidDate { get; set; }

        public string? UserId { get; set; }

        [Column(TypeName = "varchar(50)")]
        [MaxLength(250)]
        public string Method { get; set; }

        public int InvoiceId { get; set; }
        public Invoice? Invoice { get; set; }

    }
}
