using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace condogestcet97.web.Data.Entities.Financial
{
    public class IncomingInvoice : Invoice
    {
        public override InvoiceType Type { get; set; } = InvoiceType.Incoming;

        [Column(TypeName = "varchar(250)")]
        [MaxLength(250)]
        public string SupplierName { get; set; }

        [Column(TypeName = "varchar(250)")]
        [MaxLength(250)]
        public string SupplierContact { get; set; }

        public Expense? Expense { get; set; }

        public int ExpenseId { get; set; }

    }
}
