using condogestcet97.web.Data.Entities.Financial;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace condogestcet97.web.Models
{
    public class InvoiceViewModel
    {
        public InvoiceType InvoiceType { get; set; }
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }

        public DateTime DueDate { get; set; }
        public bool IsPaid { get; set; }

        public string? Description { get; set; }

        public string? SupplierName { get; set; }

        public string? SupplierContact { get; set; }

        public int? ExpenseId { get; set; }

        public string? UserId { get; set; }

        public int? QuotaId { get; set; }

        public DateTime? EmissionDate { get; set; }

        public IEnumerable<SelectListItem>? Quotas { get; set; }
        public IEnumerable<SelectListItem>? Users { get; set; }
        public IEnumerable<SelectListItem>? Expenses { get; set; }
    }
}
