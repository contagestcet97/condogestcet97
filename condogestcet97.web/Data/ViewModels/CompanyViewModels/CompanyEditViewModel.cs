using System.ComponentModel.DataAnnotations;

namespace condogestcet97.web.Data.ViewModels.CompanyViewModels
{
    public class CompanyEditViewModel
    {
        // The unique identifier for the company (required for editing)
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(25, ErrorMessage = "Name cannot exceed 25 characters.")]
        public string? Name { get; set; }

        [MaxLength(100, ErrorMessage = "Address cannot exceed 100 characters.")]
        public string? Address { get; set; }

        [MaxLength(15, ErrorMessage = "Phone number cannot exceed 15 characters.")]
        public string? Phone { get; set; }

        [MaxLength(18, ErrorMessage = "Fiscal number cannot exceed 18 characters.")]
        public string? FiscalNumber { get; set; }
    }
}

