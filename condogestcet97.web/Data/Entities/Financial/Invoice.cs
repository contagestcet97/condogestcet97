using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace condogestcet97.web.Data.Entities.Financial
{
    public enum InvoiceType
    {
        Incoming, // Utility bills, services
        Outgoing  // Quotas, assessments
    }

    public abstract class Invoice : IEntity
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }

        public DateTime DueDate { get; set; }
        public bool IsPaid { get; set; }

        [Column(TypeName = "varchar(250)")]
        [MaxLength(250)]
        public string Description { get; set; }

        public abstract InvoiceType Type { get; set; }
    }
}
