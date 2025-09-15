using condogestcet97.web.Data.Entities.Financial;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace condogestcet97.web.Models
{
    public class ReceiptViewModel
    {
        public int Id { get; set; }

        public int PaymentId { get; set; }

        public string PayeeName { get; set; }

        public string FiscalNumber { get; set; }

        public ReceiptType Type { get; set; }

        public IEnumerable<SelectListItem>? Payments { get; set; }
    }
}
