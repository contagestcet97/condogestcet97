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

        [MaxLength(250, ErrorMessage = "Address cannot exceed 250 characters.")]
        public string? Address { get; set; }

        [MaxLength(13, ErrorMessage = "Phone number cannot exceed 13 characters.")]
        public string? Phone { get; set; }

        [MaxLength(9, ErrorMessage = "Fiscal number cannot exceed 9 characters.")]
        public string? FiscalNumber { get; set; }
    }
}

