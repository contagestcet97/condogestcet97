using System.ComponentModel.DataAnnotations;

namespace condogestcet97.web.Data.ViewModels.UserViewModels
{
    public class UserDetailsViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string? Name { get; set; }

        public string? Email { get; set; }

        public bool? TwoFAEnabled { get; set; } = false; // Indicates if two-factor authentication is enabled -TODO

        [Required(ErrorMessage = "Address is required.")]
        [MaxLength(100, ErrorMessage = "Address cannot exceed 100 characters.")]
        public string? Address { get; set; }

        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Fiscal number is required for tax purposes.")]
        [MaxLength(18, ErrorMessage = "Fiscal number cannot exceed 18 characters.")]
        public string? FiscalNumber { get; set; }

        public bool EmailConfirmed { get; set; }
    }
}
