using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace condogestcet97.web.Data.Entities.Financial
{
    public class OutgoingInvoice : Invoice
    {
        public override InvoiceType Type { get; set; } = InvoiceType.Outgoing;

        public int UserId { get; set; }

        public int? QuotaId { get; set; }

        public Quota? Quota { get; set; }

        public DateTime EmissionDate { get; set; }

    }
}
