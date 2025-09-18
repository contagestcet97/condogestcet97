using condogestcet97.web.Data.Entities.Financial;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace condogestcet97.web.Models
{
    public class PaymentViewModel
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public DateTime PaidDate { get; set; }

        public int? UserId { get; set; }

        public string Method { get; set; }

        public int InvoiceId { get; set; }

        public IEnumerable<SelectListItem>? Invoices { get; set; }

    }
}
